using BLL.ViewModels;
using DAL.Data;
using DAL.Models;
using DAL.Repositories;
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
                Questions = quizzes.Questions.Select(q => new QuestionsVM
                {
                    Id = q.Id,
                    QuestionContent = q.QuestionContent,
                    QuestionType = (int)q.QuestionType,
                    Answers = q.Answers.Select(a => new AnswersVM
                    {
                        AnswerContent = a.AnswerContent,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                }).ToList()
            };

            return Ok(response);
        }

        [HttpGet]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var quiz = await dbContext.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (quiz == null)
            {
                return NotFound(new { message = "Quiz not found" });
            }

            var response = new QuizzesVM
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Description = quiz.Description,
                Subject = quiz.Subject,
                Config = quiz.Config,
                CreatedAt = quiz.CreatedAt,
                LastUpdateAt = quiz.LastUpdateAt,
                Questions = quiz.Questions.Select(q => new QuestionsVM
                {
                    Id = q.Id,
                    QuestionContent = q.QuestionContent,
                    QuestionType = (int)q.QuestionType,
                    Answers = q.Answers.Select(a => new AnswersVM
                    {
                        Id = a.Id,
                        AnswerContent = a.AnswerContent,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                }).ToList()
            };

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteQuiz(Guid id)
        {
            var quiz = dbContext.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefault(q => q.Id == id);

            if (quiz == null) return NotFound();

            // Xóa tất cả câu hỏi và câu trả lời liên quan
            dbContext.Answers.RemoveRange(quiz.Questions.SelectMany(q => q.Answers));
            dbContext.Questions.RemoveRange(quiz.Questions);

            // Xóa quiz
            dbContext.Quizzes.Remove(quiz);
            dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuiz(Guid id, updateQuizzesVM updateQuizzesVM)
        {
            // Tìm quiz cần cập nhật
            var quiz = await dbContext.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (quiz == null)
            {
                return NotFound(new { message = "Quiz not found" });
            }

            // Cập nhật các thuộc tính của quiz
            quiz.Title = updateQuizzesVM.Title;
            quiz.Description = updateQuizzesVM.Description;
            quiz.Subject = updateQuizzesVM.Subject;
            quiz.Config = updateQuizzesVM.Config;
            quiz.LastUpdateAt = DateTime.UtcNow;

            // Xóa các câu hỏi và câu trả lời cũ
            dbContext.Answers.RemoveRange(quiz.Questions.SelectMany(q => q.Answers));
            dbContext.Questions.RemoveRange(quiz.Questions);

            // Thêm câu hỏi và câu trả lời mới
            quiz.Questions = updateQuizzesVM.Questions.Select(q => new Questions
            {
                QuestionContent = q.QuestionContent,
                QuestionType = (QuestionType)q.QuestionType,
                Answers = q.Answers.Select(a => new Answers
                {
                    AnswerContent = a.AnswerContent,
                    IsCorrect = a.IsCorrect
                }).ToList()
            }).ToList();

            // Lưu thay đổi vào cơ sở dữ liệu
            await dbContext.SaveChangesAsync();

            // Chuẩn bị phản hồi
            var response = new QuizzesVM
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Description = quiz.Description,
                Subject = quiz.Subject,
                Config = quiz.Config,
                CreatedAt = quiz.CreatedAt,
                LastUpdateAt = quiz.LastUpdateAt,
                Questions = quiz.Questions.Select(q => new QuestionsVM
                {
                    Id = q.Id,
                    QuestionContent = q.QuestionContent,
                    QuestionType = (int)q.QuestionType,
                    Answers = q.Answers.Select(a => new AnswersVM
                    {
                        Id = a.Id,
                        AnswerContent = a.AnswerContent,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                }).ToList()
            };

            return Ok(response);
        }

    }

}
