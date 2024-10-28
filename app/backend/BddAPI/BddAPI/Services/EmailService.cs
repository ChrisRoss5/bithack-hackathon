using BddAPI.DTOs.Request;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BddAPI.Services;

public interface IEmailService
{
    Task Send(EmailMessage emailMessage);
}

public class MailSettings
{
    public string? DisplayName { get; set; } = null!;
    public string From { get; set; } = null!;
    public string? Username { get; set; } = null!;
    public string? Password { get; set; } = null!;
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public bool? UseSSL { get; set; }
}

public class EmailService(IOptions<MailSettings> settings) : IEmailService
{
    private readonly MailSettings _settings = settings.Value;

    public async Task Send(EmailMessage emailMessage)
    {
        var mail = new MimeMessage();
        var _body = new BodyBuilder { HtmlBody = emailMessage.Body };
        mail.From.Add(new MailboxAddress(_settings.DisplayName, _settings.From));
        mail.To.Add(MailboxAddress.Parse(emailMessage.ReceiverMail));
        mail.ReplyTo.Add(new MailboxAddress(_settings.DisplayName, _settings.From));
        mail.Subject = emailMessage.Subject ?? "Bjelovar Društveni Domovi";
        mail.Body = _body.ToMessageBody();
        using var smtp = new MailKit.Net.Smtp.SmtpClient();
        await smtp.ConnectAsync(_settings.Host, _settings.Port,
            _settings.UseSSL == true ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls);
        if (!string.IsNullOrEmpty(_settings.Username) && !string.IsNullOrEmpty(_settings.Password))
            await smtp.AuthenticateAsync(_settings.Username, _settings.Password);
        await smtp.SendAsync(mail);
        await smtp.DisconnectAsync(true);
    }
}