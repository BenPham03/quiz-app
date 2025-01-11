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

        [HttpGet("Top5right/{examId}")]
        public async Task<IActionResult> GetTop5Right(Guid examId)
        {
            return Ok(_service.top5right(examId));
        }

        [HttpGet("Top5wrong/{examId}")]
        public async Task<IActionResult> GetTop5Wrong(Guid examId)
        {
            return Ok(_service.top5wrong(examId));
        }

        [HttpGet("Rank/{examId}")]
        public async Task<IActionResult> GetRank(Guid examId)
        {
            return Ok(_service.Rank(examId));
        }

        [HttpGet("analyst/{examId}")]
        public async Task<IActionResult> GetAnalyst(Guid examId)
        {
            return Ok(_service.Analyst(examId));
        }
    }
}
