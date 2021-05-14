using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentApp.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmailAsync(string ToEmail, string FromName, string Subject, string Message);
    }
}
