using BLL.Services.EmailService;
using Google.Apis.Gmail.v1;
using Microsoft.AspNetCore.Mvc;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using MimeKit;
using BLL.ViewModels;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public class MailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        public MailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailRequestVM sendEmailRequestVM)
        {
            try
            {
                await _emailSender.SendEmailAsync(sendEmailRequestVM);
                return Ok(new { message = "Send email successful" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
