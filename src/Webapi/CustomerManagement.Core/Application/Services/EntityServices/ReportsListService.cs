using CustomerManagement.Core.Application.Dtos.ApiModelWrappers;
using CustomerManagement.Core.Application.Interfaces.EntityServices;
using CustomerManagement.Core.Domain.Entities;
using CustomerManagement.Core.Domain.Interfaces.Repositories;
using CustomerManagement.Utility.Extensions;
using Microsoft.AspNetCore.Http;

namespace CustomerManagement.Core.Application.Services.EntityServices;

public class ReportsListService : IReportsListService
{
    private readonly IReportRepository _reportRepository;
    private readonly IReportFileRepository _reportFileRepository;

    public ReportsListService(IReportRepository reportRepository, IReportFileRepository reportFileRepository)
    {
        _reportRepository = reportRepository;
        _reportFileRepository = reportFileRepository;
    }

    public Stream GetFileByIdInStream(int id)
    {
        Stream stream = new MemoryStream();
        var reportFile = _reportFileRepository.GetById(id);

        stream.Write(reportFile.File);

        return stream;
    }

    ObjectResponse<List<Report>> IReportsListService.GetAll()
    {
        int code;
        var data = _reportRepository.GetAll();

        if (data.IsNullOrEmpty())
        {
            code = 404;
            return new ObjectResponse<List<Report>>(code);
        }

        code = 200;
        var response = new ObjectResponse<List<Report>>(data, code);
        return response;
    }
}
