using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using HRM.API.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;



public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailVerificationAsync(string email, string token)
    {
        var smtpHost = _configuration["EmailSettings:SmtpHost"];
        var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
        var smtpUsername = _configuration["EmailSettings:Username"];
        var smtpPassword = _configuration["EmailSettings:Password"];
        var fromEmail = _configuration["EmailSettings:FromEmail"];

        var verificationLink = $"https://localhost:7006/api/Authen/VerifyEmail?token={token}";

        var message = new MailMessage
        {
            From = new MailAddress(fromEmail),
            Subject = "Xác thực email của bạn",
            IsBodyHtml = true,
            Body = $@"
                <h2>Xác thực tài khoản</h2>
                <p>Vui lòng nhấp vào liên kết bên dưới để xác thực email của bạn:</p>
                <a href='{verificationLink}'>Xác thực ngay</a>
                <p>Liên kết này sẽ hết hạn sau 24 giờ.</p>"
        };
        message.To.Add(email);

        using var smtpClient = new SmtpClient(smtpHost, smtpPort)
        {
            Credentials = new NetworkCredential(smtpUsername, smtpPassword),
            EnableSsl = true
        };

        await smtpClient.SendMailAsync(message);
    }

    
}