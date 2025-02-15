﻿    using DAL.Data;
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
using System.Xml;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public IQueryable<Quizzes> GetFilter(bool status = true, bool isDescending = true, string includeProperties = "")
        {
            IQueryable<Quizzes> quiz = _dbContext.Quizzes;
            Expression<Func<Quizzes, bool>>? filter = null;
            filter = qz => qz.Status == status;
            Func<IQueryable<Quizzes>, IOrderedQueryable<Quizzes>> order = null;
            order = qz => qz.OrderByDescending(q => q.CreatedAt);
            var quizzes = order(quiz.Where(filter));
            IQueryable<Quizzes> queryable = (IOrderedQueryable<Quizzes>)quizzes;
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    queryable = queryable.Include(property);
                }
            }
            return queryable;
            
        }
       public IQueryable<Quizzes> GetDoneQuiz(string userId)
        { 
            var quiz = _dbContext.Quizzes.Join(_dbContext.Attempts.Where(a  => a.UserId == userId),
                qz => qz.Id,
                at => at.QuizzId,
                (qz, at) => qz);
            return quiz;

        }
        public async Task<List<Quizzes>> GetItems(string item)
        {
            if (Guid.TryParse(item, out var guid))
            {
                var quiz = await _dbContext.Quizzes.Where(c => c.Id == guid).ToListAsync();
                return quiz;
            }
            return await _dbContext.Quizzes.Where(c => c.Title.ToLower().Contains(item.ToLower())).ToListAsync();
        }
    }
}
