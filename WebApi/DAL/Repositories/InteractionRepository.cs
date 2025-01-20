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
    public class InteractionRepository : GenericRepository<Interactions>, IInteractionRepository
    {
        public InteractionRepository(DataDbContext dbContext) : base(dbContext)
        {
        }
        public bool InteractionExist(Guid quizId, string userId)
        {
            return _dbContext.Interactions.Where(i => i.QuizzId == quizId && i.UserId == userId) != null;
        }
        public Interactions? GetByQuizIdUserId(Guid quizId, string userId)
        {
            return _dbContext.Interactions.Where(i => i.QuizzId == quizId && i.UserId == userId).FirstOrDefault();
        }
    }
}
