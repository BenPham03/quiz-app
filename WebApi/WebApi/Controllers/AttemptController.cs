using BLL.Extentions;
using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public class AttemptController : ControllerBase
    {
        private readonly AttemptService _attemptService;
        private readonly UserManager<AppUser> _userManager;
        public AttemptController(AttemptService attemptService, UserManager<AppUser> userManager)
        {
            _attemptService = attemptService;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var attempts = await _attemptService.GetAllAsync();
            return Ok(attempts);
        }
        [HttpGet("get-most-recent")]
        public async Task<IActionResult> GetMostRecent(int pageIndex = 1, int pageSize = 6)
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            if(user!= null)
            {
                var attempts = await _attemptService.GetMostRecent(user.Id,pageIndex,pageSize);
                return Ok(attempts);
            }
            return Unauthorized();
        }
    }
}
