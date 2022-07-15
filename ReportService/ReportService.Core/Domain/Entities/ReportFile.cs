namespace ReportService.Core.Domain.Entities;

public class ReportFile
{
    public int Id { get; set; }
    public byte[] File { get; set; }

    public Report Report { get; set; }
}