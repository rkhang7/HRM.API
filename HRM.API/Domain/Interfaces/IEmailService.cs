﻿namespace HRM.API.Domain.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailVerificationAsync(string email, string token);
        Task SendEmailForgotPasswordAsync(string email, string password);
    }
}
