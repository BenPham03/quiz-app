using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserAnswerRepository : GenericRepository<UserAnswers>, IUserAnswerRepository
    {
        public UserAnswerRepository(DataDbContext dbContext) : base(dbContext)
        {
        }
        public float Scoring(List<UserAnswers> userAnswers)
        {
            int numberOfQuestion = userAnswers.Count();
            var rightAnwser = userAnswers.Join(_dbContext.Answers.Where(an => an.IsCorrect == true),
                ua => ua.AnswerId,
                aw => aw.Id,
                (ua, aw) => ua).Count();
            var score = ((float)rightAnwser / (float)numberOfQuestion) * 100;
            return score;
        }
        public void AddRange(List<UserAnswers> userAnswers)
        {
            _dbContext.UserAnswers.AddRange(userAnswers);
        }
    }
}
