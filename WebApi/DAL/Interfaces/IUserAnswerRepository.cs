using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserAnswerRepository : IGenericRepository<UserAnswers>
    {
        float Scoring(List<UserAnswers> userAnswers);
        public void AddRange(List<UserAnswers> userAnswers);
    }
}
