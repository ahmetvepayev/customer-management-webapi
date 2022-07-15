using System.Data;
using ClosedXML.Excel;
using ReportService.Core.Application.Interfaces;
using ReportService.Core.Domain.Entities;
using ReportService.Core.Domain.Interfaces;
using ReportService.Core.Domain.Interfaces.Repositories;

namespace ReportService.Core.Application.Services;

public class CreateReportService : ICreateReportService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IReportRepository _reportRepository;

    public CreateReportService(IReportRepository reportRepository, IUnitOfWork unitOfWork, ICustomerRepository customerRepository)
    {
        _reportRepository = reportRepository;
        _unitOfWork = unitOfWork;
        _customerRepository = customerRepository;
    }

    public void CreateCustomersByCityReport()
    {
        using(var memstr = new MemoryStream())
        {
            var workBook = new XLWorkbook();
            var dataSet = new DataSet();
            
            dataSet.Tables.Add(GetCustomersByCityTable("CustomersByCity"));

            workBook.Worksheets.Add(dataSet);

            workBook.SaveAs(memstr);

            Report report = new Report{
                Name = "customers-by-city" + Guid.NewGuid().ToString(),
                CreateDate = DateTime.UtcNow,

                ReportFile = new ReportFile{
                    File = memstr.ToArray()
                }
            };

            _reportRepository.Add(report);

            _unitOfWork.SaveChanges();
        }

    }

    public void CreateTop5CustomersReport()
    {
        using(var memstr = new MemoryStream())
        {
            var workBook = new XLWorkbook();
            var dataSet = new DataSet();
            
            dataSet.Tables.Add(GetTop5CustomersTable("Top5Customers"));

            workBook.Worksheets.Add(dataSet);

            workBook.SaveAs(memstr);

            Report report = new Report{
                Name = "top-5-customers" + Guid.NewGuid().ToString(),
                CreateDate = DateTime.UtcNow,

                ReportFile = new ReportFile{
                    File = memstr.ToArray()
                }
            };

            _reportRepository.Add(report);

            _unitOfWork.SaveChanges();
        }
    }

    private DataTable GetCustomersByCityTable(string tableName)
    {
        var customers = _customerRepository.GetAll();

        DataTable table = new(tableName);
        table.Columns.Add("City", typeof(string));
        table.Columns.Add("No. of Customers", typeof(int));

        Dictionary<string, int> dict = new();
        customers.ForEach(c => {
            if(!dict.ContainsKey(c.City))
            {
                dict.Add(c.City, 1);
            }
            else
            {
                dict[c.City] = dict[c.City] + 1;
            }
        });
        var keysAndValues = (dict.Keys.Zip(dict.Values, (c, n) => new { City = c, NoOfCustomers = n })).OrderBy(x => x.City);
        foreach(var keyvalue in keysAndValues)
        {
            table.Rows.Add(keyvalue.City, keyvalue.NoOfCustomers);
        }

        return table;
    }

    private DataTable GetTop5CustomersTable(string tableName)
    {
        var customers = _customerRepository.GetTopCustomers(5);

        DataTable table = new(tableName);
        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("Firstname", typeof(string));
        table.Columns.Add("LastName", typeof(string));
        table.Columns.Add("Email", typeof(string));
        table.Columns.Add("Phone", typeof(string));
        table.Columns.Add("City", typeof(string));
        table.Columns.Add("Total number of Transactions", typeof(int));
        table.Columns.Add("Total amount of Transactions", typeof(decimal));

        customers.ForEach(c => {
            table.Rows.Add(
                c.Id,
                c.Firstname,
                c.LastName,
                c.Email,
                c.Phone,
                c.City,
                c.CommercialTransactions.Count(),
                c.CommercialTransactions.Select(ct => ct.Amount).Sum()
            );
        });

        return table;
    }
}