using BLL.Extentions;
using BLL.Mappers;
using BLL.Services;
using BLL.ViewModels.Quiz;
using DAL.Infratructure;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Xml;

namespace WebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly QuizService _quizService;
        private readonly UserManager<AppUser> _userManager;
        public QuizController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, QuizService quizService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _quizService = quizService;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var quizzes = await _quizService.GetAllAsync();
            return Ok(quizzes);
        }
        [HttpGet("get-filter")]
        public async Task<IActionResult> GetFilter(int pageIndex = 1, int pageSize = 6, bool status = true, bool isDescending = true)
        {
            var quizzes = await _quizService.GetFilter(pageIndex: pageIndex, pageSize: pageSize, status: status, isDescending: isDescending);
            return Ok(quizzes);
        }
        [HttpGet("get-quiz-done")]
        public async Task<IActionResult> GetQuizDone(int pageIndex = 1, int pageSize = 20)
        {
            var userName = User.GetUsername();
            var user = await _userManager.FindByNameAsync(userName);
            if(user == null)
            {
                return Unauthorized();
            }
            var quizzes = await _quizService.GetDone(userId: user.Id,pageIndex: pageIndex, pageSize: pageSize);
            return Ok(quizzes);
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var quiz = await _quizService.GetByIdAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }
            return Ok(quiz);
        }
        [HttpGet("getItems")]
        public async Task<IActionResult> GetItems(string items)
        {
            var quiz = await _quizService.GetItems(items);
            return Ok(quiz);
        }
        [Authorize]
        [HttpPost("add-new-quiz")]
        public async Task<IActionResult> Create([FromBody] CreateQuizVM model)
        {
            try
            {
                var userName = User.GetUsername();
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    var quiz = model.ToQuizFromCreateVM(user.Id);
                    await _quizService.AddAsync(quiz);
                    return Ok(quiz);
                }
                return BadRequest();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        [Authorize]
        [HttpDelete("delete-quiz")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _quizService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("update-quiz/{quizId}")]
        public async Task<IActionResult> Update( Guid quizId, UpdateQuizVM model)
        {
            try
            {
                var quizUpdated = await _quizService.UpdateAsync(quizId, model);
                if(quizUpdated == null)
                {
                    return NotFound();
                }
                return Ok(quizUpdated);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
