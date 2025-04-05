using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using TodoList.Application.Interfaces.Services;
using TodoList.Infrastructure.Options;

namespace TodoList.Infrastructure.Services;

public class EmailService: IEmailService
{
    private readonly EmailOptions _options;
    
    private readonly ILogger<EmailService> _logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        _options = configuration.GetSection("EmailSettings").Get<EmailOptions>()!;
        _logger = logger;
    }
    
    public async Task SendEmailAsync(string email, string subject, string messageBody)
    {
        try
        {
            using var smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync(_options.Host, _options.Port, true);
            await smtpClient.AuthenticateAsync(_options.EmailAddress, _options.Password);
            await smtpClient.SendAsync(GenerateMessage(email, subject, messageBody));
            await smtpClient.DisconnectAsync(true);
        }
        catch (SmtpProtocolException ex)
        {
            _logger.LogError("SMTP server could not be established: {0}", ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to send mail message: {0}", ex.Message);
            throw;
        }
    }

    private MimeMessage GenerateMessage(string email, string subject, string messageBody)
    {
        return new MimeMessage
        {
            From = {new MailboxAddress("TodoList", _options.EmailAddress)},
            To = {new MailboxAddress("", email)},
            Subject = subject,
            Body = new BodyBuilder { HtmlBody = messageBody }.ToMessageBody()
        };
    }
    
    public string GenerateRandomCode() => new Random().Next(100000, 999999).ToString();
}