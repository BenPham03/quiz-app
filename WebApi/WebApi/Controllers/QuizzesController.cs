using BLL.Services;
using BLL.ViewModels;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly QuizzesService _quizzesService;

        public QuizzesController(QuizzesService quizzesService)
        {
            _quizzesService = quizzesService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuizzes([FromBody] addQuizzesVM addQuizzesVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (addQuizzesVM == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var quizzes = new Quizzes
                {
                    Title = addQuizzesVM.Title,
                    Config = addQuizzesVM.Config,
                    Description = addQuizzesVM.Description,
                    Subject = addQuizzesVM.Subject,
                    Questions = addQuizzesVM.Questions.Select(q => new Questions
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

                var result = await _quizzesService.AddAsync(quizzes);
                if (result > 0)
                {
                    return Ok(new { message = "Quiz created successfully." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return StatusCode(500, "Failed to create quiz.");
        }
        [HttpGet]
        public async Task<IActionResult> GetQuizzesAsync(int page = 1, int pageSize = 5)
        {
            // Include cả Questions và Answers
            var quizzes = await _quizzesService.GetAsync(includeProperties: "Questions,Questions.Answers", pageIndex: page, pageSize: pageSize);

            if (quizzes == null || !quizzes.Items.Any())
            {
                return NotFound(new { message = "No quizzes found." });
            }

            return Ok(new
            {
                data = quizzes.Items.Select(q => new
                {
                    q.Id,
                    q.Title,
                    q.Description,
                    q.Subject,
                    q.Config,
                    q.Status,
                    Questions = q.Questions.Select(qs => new
                    {
                        qs.Id,
                        qs.QuestionContent,
                        qs.QuestionType,
                        Answers = qs.Answers.Select(ans => new
                        {
                            ans.Id,
                            ans.AnswerContent,
                            ans.IsCorrect
                        })
                    }),
                    q.CreatedAt,
                    q.LastUpdateAt,
                    q.UserId
                }),
                totalItems = quizzes.TotalCount
            });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var quiz = await _quizzesService.GetByIdAsync(id);
            if (quiz == null)
            {
                return NotFound(new { message = "Quiz not found." });
            }

            return Ok(new
            {
                quiz.Id,
                quiz.Title,
                quiz.Description,
                quiz.Subject,
                quiz.Config,
                quiz.Status,
                Questions = quiz.Questions.Select(q => new
                {
                    q.Id,
                    q.QuestionContent,
                    q.QuestionType,
                    Answers = q.Answers.Select(a => new
                    {
                        a.Id,
                        a.AnswerContent,
                        a.IsCorrect
                    })
                }),
                quiz.CreatedAt,
                quiz.LastUpdateAt,
                quiz.UserId
            });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(Guid id)
        {
            var result = await _quizzesService.DeleteAsync(id);
            if (result)
            {
                return NoContent();
            }

            return NotFound(new { message = "Quiz not found or failed to delete." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuiz(Guid id, updateQuizzesVM updateQuizzesVM)
        {
            var quiz = await _quizzesService.GetByIdAsync(id);
            if (quiz == null)
            {
                return NotFound(new { message = "Quiz not found." });
            }

            // Update quiz properties
            quiz.Title = updateQuizzesVM.Title;
            quiz.Description = updateQuizzesVM.Description;
            quiz.Subject = updateQuizzesVM.Subject;
            quiz.Config = updateQuizzesVM.Config;
            quiz.LastUpdateAt = DateTime.UtcNow;

            // Update questions and answers
            quiz.Questions.Clear();
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

            var result = await _quizzesService.UpdateAsync(quiz);
            if (result)
            {
                return Ok(new { message = "Quiz updated successfully." });
            }

            return StatusCode(500, "Failed to update quiz.");
        }
    }
}
