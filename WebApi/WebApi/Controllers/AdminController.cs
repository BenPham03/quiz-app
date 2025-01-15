using BLL.Services;
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
    }
}
