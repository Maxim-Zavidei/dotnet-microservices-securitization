namespace Jobs.Identity.Services;

public interface IEmailService
{
    void SendEmail(string address);
}
