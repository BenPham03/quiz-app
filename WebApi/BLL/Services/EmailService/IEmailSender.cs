using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.EmailService
{
    public interface IEmailSender
    {
        Task SendEmailAsync(SendEmailRequestVM sendEmailRequestNM);
    }
}
