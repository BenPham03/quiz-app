using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.EmailService
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(SendEmailRequestVM sendEmailRequestVM)
        {
            var mail = "benpham10112003@gmail.com";
            var pw = "htuo dgtw fdyw ccas";
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };
            return client.SendMailAsync(
                new MailMessage(from: mail,
                                 to: sendEmailRequestVM.Email,
                                 sendEmailRequestVM.Subject,
                                 sendEmailRequestVM.Message
                ));
        }
    }
}
