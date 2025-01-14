using BLL.ViewModels;
using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly DataDbContext dbContext;

        public QuizzesController(DataDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpPost]
        public async Task<IActionResult> CreateQuizzes(addQuizzesVM addquizzesVM)
        {
            var quizzes = new Quizzes
            {
                Title = addquizzesVM.Title,
                Config = addquizzesVM.Config,
                Description = addquizzesVM.Description,
                Subject = addquizzesVM.Subject,
                Questions = addquizzesVM.Questions.Select(q => new Questions
                {
                    QuestionContent = q.QuestionContent,
                    QuestionType = (QuestionType)q.QuestionType,
                    Answers = q.Answers.Select(a => new Answers
                    {
                        AnswerContent = a.AnswerContent,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                }).ToList()

            };
            await dbContext.Quizzes.AddAsync(quizzes);
            await dbContext.SaveChangesAsync();
            var response = new QuizzesVM
            {
                Id = quizzes.Id,
                Title = quizzes.Title,
                Description = quizzes.Description,
                Subject = quizzes.Subject,
                Config = quizzes.Config,
                CreatedAt = quizzes.CreatedAt,
                LastUpdateAt = quizzes.LastUpdateAt,
                UserId = quizzes.UserId,
                User = quizzes.User,
                Questions = quizzes.Questions,
                Interactions = quizzes.Interactions,
                Attempts = quizzes.Attempts
            };

            return Ok();
        }
        [HttpGet("quizzes")]
        public IActionResult GetQuizzes(int page = 1, int pageSize = 5)
        {
            var quizzes = dbContext.Quizzes
                .Select(q => new
                {
                    q.Id,
                    q.Title,
                    q.Description,
                    q.Subject,
                    q.CreatedAt,
                    q.LastUpdateAt,
                    QuestionCount = dbContext.Questions.Count(ques => ques.Id == q.Id)
                })
                .OrderByDescending(q => q.CreatedAt) // Sắp xếp mới nhất
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalItems = dbContext.Quizzes.Count();
            return Ok(new { data = quizzes, totalItems });
        }
        [HttpDelete("quizzes/{id}")]
        public IActionResult DeleteQuiz(Guid id)
        {
            var quiz = dbContext.Quizzes.FirstOrDefault(q => q.Id == id);
            if (quiz == null) return NotFound();

            // Xóa tất cả câu hỏi liên quan
            var questions = dbContext.Questions.Where(q => q.Id == id).ToList();
            foreach (var question in questions)
            {
                // Xóa tất cả câu trả lời liên quan
                var answers = dbContext.Answers.Where(a => a.QuestionId == question.Id).ToList();
                dbContext.Answers.RemoveRange(answers);
            }
            dbContext.Questions.RemoveRange(questions);

            // Xóa quiz
            dbContext.Quizzes.Remove(quiz);
            dbContext.SaveChanges();

            return NoContent();
        }


    }
}
