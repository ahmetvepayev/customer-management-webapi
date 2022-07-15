namespace ReportService.Core.Domain.Entities;

public class Report
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreateDate { get; set; }

    public ReportFile ReportFile { get; set; }
}