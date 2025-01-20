using BLL.Extentions;
using BLL.Mappers;
using BLL.Services;
using BLL.ViewModels.Interaction;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public class InteractionController : ControllerBase
    {
        private readonly InteractionService _interactionService;
        private readonly UserManager<AppUser> _userManager;
        public InteractionController(InteractionService interactionService, UserManager<AppUser> userManager)
        {
            _interactionService = interactionService;
            _userManager = userManager;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var interaction = await _interactionService.GetAllAsync();
            return Ok(interaction);
        }
        [HttpPost("add-new-interaction")]
        public async Task<IActionResult> Create([FromBody] CreateInteractionRequestVM model)
        {
            var username = User.GetUsername();

            if (username != null)
            {
                var user = await _userManager.FindByNameAsync(username);

                if (_interactionService.InteractionExist(model.QuizzId, user.Id))
                {
                    var interaction = model.ToInteractionFromCreate(user.Id);
                    await _interactionService.AddAsync(interaction);
                    return Ok(interaction);
                }
            }
            return Unauthorized();
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid quizId)
        {
            try
            {
                var username = User.GetUsername();
                if (username != null)
                {
                    var user = await _userManager.FindByNameAsync(username);
                    var result = await _interactionService.DeleteByQuizIdUserId(quizId, user.Id);
                    if (result != null)
                    {
                        return NoContent();
                    }
                    return NotFound();
                }
                return Unauthorized();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
