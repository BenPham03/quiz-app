using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesIdController : ControllerBase
    {
        private readonly DataDbContext _context;

        public QuizzesIdController(DataDbContext context)
        {
            _context = context;
        }

        // API lấy chi tiết quiz theo quizId
        [HttpGet("quizzes/{id}")]
        public ActionResult<Quizzes> GetQuizDetails(Guid id)
        {
            var quiz = _context.Quizzes
                .Include(q => q.Questions) // Lấy danh sách câu hỏi
                    .ThenInclude(q => q.Answers) // Lấy danh sách câu trả lời của từng câu hỏi
                .Where(q => q.Id == id)
                .FirstOrDefault();

            if (quiz == null)
            {
                return NotFound(); // Nếu không tìm thấy quiz
            }

            // Trả về chi tiết quiz bao gồm câu hỏi và câu trả lời
            return Ok(new
            {
                quiz.Id,
                quiz.Title,
                quiz.Description,
                Questions = quiz.Questions.Select(q => new
                {
                    q.Id,
                    q.QuestionContent,
                    Answers = q.Answers.Select(a => new
                    {
                        a.Id,
                        a.AnswerContent,
                        a.IsCorrect // Chỉ định đáp án đúng
                    })
                })
            });
        }
    }
}
