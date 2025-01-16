using BLL.Mappers;
using BLL.Services;
using BLL.ViewModels;
using BLL.ViewModels.Attempt;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public class DoExamController : ControllerBase
    {
        private readonly DoExamService _doExamService;
        public DoExamController(DoExamService doExamService)
        {
            _doExamService = doExamService;
        }
        [HttpPost("submit-exam")]
        public async Task<IActionResult> Submit(SubmitExamRequestVM submitExamRequest)
        {
            try
            {
                var createAttempt = submitExamRequest.Attempt.ToAttemptFromCreate();
                await _doExamService.SubmitAsync(submitExamRequest.UserAnswers, createAttempt);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
