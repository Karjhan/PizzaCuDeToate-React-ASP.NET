using PizzaCuDeToateAPI.Models;

namespace PizzaCuDeToateAPI.Services;

public interface IEmailService
{
    void SendEmail(MailMessage message);
}