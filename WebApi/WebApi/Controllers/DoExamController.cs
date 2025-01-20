using BLL.Extentions;
using BLL.Mappers;
using BLL.Services;
using BLL.ViewModels;
using BLL.ViewModels.Attempt;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public class DoExamController : ControllerBase
    {
        private readonly DoExamService _doExamService;
        private readonly UserManager<AppUser> _userManager;
        public DoExamController(DoExamService doExamService, UserManager<AppUser> userManager)
        {
            _doExamService = doExamService;
            _userManager = userManager;
        }
        [HttpPost("submit-exam")]
        public async Task<IActionResult> Submit(SubmitExamRequestVM submitExamRequest)
        {
            try
            {
                var userName = User.GetUsername();
                if (userName != null)
                {
                    var user = await _userManager.FindByNameAsync(userName);
                    var createAttempt = submitExamRequest.Attempt.ToAttemptFromCreate(user.Id);
                    var userAnswer = submitExamRequest.UserAnswers?.Select(c => c.ToUserAnswerFromCreate()).ToList() ?? new List<UserAnswers>(); ;
                    await _doExamService.SubmitAsync(userAnswer, createAttempt);
                    return Ok();
                }
                else
                {
                    var createAttempt = submitExamRequest.Attempt.ToAttemptFromCreate();
                    var userAnswer = submitExamRequest.UserAnswers.Select(c => c.ToUserAnswerFromCreate()).ToList();
                    await _doExamService.SubmitAsync(userAnswer, createAttempt);
                    return Ok();
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
