using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class QuestionRepository : GenericRepository<Questions>, IQuestionRepository
    {
        public QuestionRepository(DataDbContext dbContext) : base(dbContext)
        {
            
        }
        public async Task<List<Questions>> GetByQuizIdAsync(Guid quizId)
        {
            var questions = await _dbContext.Questions.Include(q => q.Answers).Where(q => q.QuizzId == quizId).ToListAsync();
            return questions;
        }
    }
}
