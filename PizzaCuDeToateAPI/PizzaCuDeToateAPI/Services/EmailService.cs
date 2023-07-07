using System;
using MimeKit;
using PizzaCuDeToateAPI.Models;
using MailMessage = PizzaCuDeToateAPI.Models.MailMessage;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace PizzaCuDeToateAPI.Services;

public class EmailService : IEmailService
{
    private readonly EmailConfiguration _emailConfiguration;

    public EmailService(EmailConfiguration emailConfiguration)
    {
        _emailConfiguration = emailConfiguration;
    }
    
    public void SendEmail(MailMessage message)
    {
        var emailMessage = CreateMailMessage(message);
        Send(emailMessage);
    }

    private MimeMessage CreateMailMessage(MailMessage message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("email",_emailConfiguration.From));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
        return emailMessage;
    }

    private void Send(MimeMessage message)
    {
        using var client = new SmtpClient();
        try
        {
            client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            client.Authenticate(_emailConfiguration.Username, _emailConfiguration.Password);
            client.Send(message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            client.Disconnect(true);
            client.Dispose();
        }
    }
}