using BLL.Services;
using BLL.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ReportService _service;
        public ReportController(ReportService reportService)
        {
            _service = reportService;
        }

        [HttpGet("quizz-list")]
        public async Task<IActionResult> GetQuizzList()
        {
            return Ok(await _service.GetQuizzes());
        }

        [HttpGet("top-5-right/{examId}")]
        public async Task<IActionResult> GetTop5Right(Guid examId)
        {
            return Ok(await _service.top5right(examId));
        }

        [HttpGet("top-5-wrong/{examId}")]
        public async Task<IActionResult> GetTop5Wrong(Guid examId)
        {
            return Ok(await _service.top5wrong(examId));
        }

        [HttpGet("rank/{examId}")]
        public async Task<IActionResult> GetRank(Guid examId)
        {
            return Ok(await _service.Rank(examId));
        }

        [HttpGet("analyst/{examId}")]
        public async Task<IActionResult> GetAnalyst(Guid examId)
        {
            return Ok(await _service.Analyst(examId));
        }
    }
}
