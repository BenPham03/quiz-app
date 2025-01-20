using BLL.Extentions;
using BLL.Services;
using BLL.ViewModels;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly QuizzesService _quizzesService;
        private readonly UserManager<AppUser> _userManager;
        public QuizzesController(QuizzesService quizzesService, UserManager<AppUser> userManager)
        {
            _quizzesService = quizzesService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuizzes(addQuizzesVM addQuizzesVM)
        {
            var userName = User.GetUsername();
            var user = await _userManager.FindByNameAsync(userName);
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
                if (user != null)
                {
                    var quizzes = new Quizzes
                    {
                        Title = addQuizzesVM.Title,
                        Config = addQuizzesVM.Config,
                        Description = addQuizzesVM.Description,
                        UserId = user.Id,
                        Questions = addQuizzesVM.Questions.Select(q => new Questions
                        {
                            QuestionContent = q.QuestionContent,
                            QuestionType = (QuestionType)q.QuestionType,
                            Answers = q.Answers.Select(a => new Answers
                            {
                                AnswerContent = a.AnswerContent,
                                IsCorrect = a.IsCorrect
                            }).ToList()
                        }).ToList(),
                        Status = addQuizzesVM.Status
                    };

                    var result = await _quizzesService.AddAsync(quizzes);
                    if (result > 0)
                    {
                        var response = new QuizzesVM
                        {
                            Id = quizzes.Id,
                            Title = quizzes.Title,
                            Description = quizzes.Description,
                            Config = quizzes.Config,
                            CreatedAt = quizzes.CreatedAt,
                            LastUpdateAt = quizzes.LastUpdateAt,
                            UserId = quizzes.UserId,
                            User = quizzes.User,
                            Questions = quizzes.Questions.Select(q => new QuestionsVM
                            {
                                Id = q.Id,
                                QuestionContent = q.QuestionContent,
                                QuestionType = (QuestionType)q.QuestionType,
                                Answers = q.Answers.Select(a => new AnswersVM
                                {
                                    AnswerContent = a.AnswerContent,
                                    IsCorrect = a.IsCorrect
                                }).ToList()
                            }).ToList()
                        };

                        return Ok(response);
                    }
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
            try
            {
                var result = await _quizzesService.UpdateQuizAsync(id, updateQuizzesVM);
                if (result)
                {
                    return Ok(new { message = "Quiz đã được cập nhật thành công." });
                }

                return StatusCode(500, new { message = "Cập nhật Quiz thất bại." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }


    }
}
