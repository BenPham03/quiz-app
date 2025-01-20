using BLL.Services.Base;
using DAL.Infratructure;
using DAL.Models;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class AdminService : BaseService<AppUser>
    {
        public AdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<List<UserVM>> GetUserList()
        {
            return await _unitOfWork.Admin.GetUserList();
        }
        public async Task<int> GetUserOnlineCount()
        {
            return await _unitOfWork.Admin.GetUserOnlineCount();
        }
        public async Task<int> GetNewUserCount()
        {
            return await _unitOfWork.Admin.GetNewUserCount();
        }
        public async Task SendEmail(string message)
        {
            var mailList = await _unitOfWork.Admin.GetUserList();
            var mailList2 = mailList.Select(u => u.Gmail).ToList();

            using (var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("benpham10112003@gmail.com", "htuo dgtw fdyw ccas"),
                EnableSsl = true,
            })
            {
                foreach (var email in mailList2)
                {
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("benpham10112003@gmail.com"),
                        Subject = "Quizz Announce !!",
                        Body = message,
                        IsBodyHtml = true,
                    };
                    mailMessage.To.Add(email);

                    try
                    {
                        await smtpClient.SendMailAsync(mailMessage);
                        Console.WriteLine($"Email sent to {email}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to send email to {email}: {ex.Message}");
                    }
                }
            }
        }
        public async Task<bool> DeleteAsync(string id)
        {
            if (id != String.Empty)
            {
                bool temp=await _unitOfWork.Admin.DeleteUser(id);
                return await _unitOfWork.SaveChangesAsync() > 0 ? true : false;
            }
            throw new ArgumentNullException(nameof(id));
        }
    }
}
