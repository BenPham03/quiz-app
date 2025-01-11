using BLL.ViewModels;
using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                Questions = addquizzesVM.Questions

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

    }
}
