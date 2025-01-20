using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionService _questionService;
        public QuestionController(QuestionService questionService)
        {
            _questionService = questionService;
        }
        [HttpGet("get-by-quizId")]
        public async Task<IActionResult> GetByQuizId(Guid quizId)
        {
            var questions = await _questionService.GetByQuizId(quizId);
            return Ok(questions);
        }
    }
}
