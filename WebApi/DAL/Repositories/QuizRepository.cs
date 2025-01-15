using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class QuizRepository : GenericRepository<Quizzes>, IQuizRepository
    {
        private readonly DataDbContext _dbContext;
        public QuizRepository(DataDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Quizzes?> UpdateAsync(Guid id, Quizzes model)
        {
            var quiz = await _dbContext.Quizzes.FirstOrDefaultAsync(q => q.Id == id);
            if (quiz != null)
            {
                quiz.Title = model.Title;
                quiz.Description = model.Description;
                quiz.Config = model.Config;
                quiz.LastUpdateAt = DateTime.Now;
                quiz.Status = model.Status;
                await _dbContext.SaveChangesAsync();
                return quiz;
            }
            return null;
        }
        public IQueryable<Quizzes> GetFilter(bool status = true, bool isDescending = true)
        {
            IQueryable<Quizzes> quiz = _dbContext.Quizzes;
            Expression<Func<Quizzes, bool>>? filter = null;
            filter = qz => qz.Status == status;
            Func<IQueryable<Quizzes>, IOrderedQueryable<Quizzes>> order = null;
            order = qz => qz.OrderByDescending(q => q.CreatedAt);
            return order(quiz.Where(filter));
            
        }
       public IQueryable<Quizzes> GetDoneQuiz(string userId)
        { 
            var quiz = _dbContext.Quizzes.Join(_dbContext.Attempts.Where(a  => a.UserId == userId),
                qz => qz.Id,
                at => at.QuizzId,
                (qz, at) => qz);
            return quiz;

        }
    }
}
