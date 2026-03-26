using MailKit.Net.Smtp;
using MimeKit;

namespace notes_api_app.app.Services;

public class EmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendOtpEmailAsync(string email, string otpCode)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Notes API", _configuration["EmailSettings:FromEmail"]));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = "Your OTP Code";

        message.Body = new TextPart("plain")
        {
            Text = $"Your OTP code is: {otpCode}. It expires in 10 minutes."
        };

        using var client = new SmtpClient();
        await client.ConnectAsync(_configuration["EmailSettings:SmtpServer"], int.Parse(_configuration["EmailSettings:SmtpPort"]), MailKit.Security.SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_configuration["EmailSettings:Username"], _configuration["EmailSettings:Password"]);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}