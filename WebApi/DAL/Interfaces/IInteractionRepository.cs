using DAL.Models;
using DAL.Repositories;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IInteractionRepository : IGenericRepository<Interactions>
    {
        public bool InteractionExist(Guid quizId, string userId);
        public Interactions? GetByQuizIdUserId(Guid quizId, string userId);
    }
}
