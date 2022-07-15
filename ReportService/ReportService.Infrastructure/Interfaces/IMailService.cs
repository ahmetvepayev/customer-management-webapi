namespace ReportService.Infrastructure.Interfaces;

public interface IMailService
{
    void SendFileToAdminRoles(string subject, string body, byte[] file);
}