using BLL.Services;
using BLL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _service;

        public AdminController(AdminService service)
        {
            _service = service;
        }

        [HttpGet("get-list")]
        public async Task<IActionResult> GetUserList()
        {
            return Ok(await _service.GetUserList());
        }

        [HttpGet("get-new-user-count")]
        public async Task<IActionResult> GetNewUserCount()
        {
            return Ok(await _service.GetNewUserCount());
        }

        [HttpGet("get-user-online-count")]
        public async Task<IActionResult> GetUserOnlineCount()
        {
            return Ok(await _service.GetUserOnlineCount());
        }

        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            return Ok(await _service.DeleteAsync(id));
        }

        [HttpPost("send-mail")]
        public async Task<IActionResult> SendMail([FromBody] MailVM message)
        {
            try
            {
                await _service.SendEmail(message.Message); 
                return Ok(new { Message = "Emails sent successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
