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
    public class AttemptRepository : GenericRepository<Attempts>, IAttemptRepository
    {
        public AttemptRepository(DataDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<Attempts>> GetMostRecent(string  userId)
        {
            var attems = await _dbContext.Attempts.Include(c => c.Quizzes).Where(at => at.UserId == userId).OrderByDescending(c => c.AttemptAt).ToListAsync();
            return attems;
            
        }
    }
}
