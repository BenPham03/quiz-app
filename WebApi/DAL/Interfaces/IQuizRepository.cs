using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IQuizRepository : IGenericRepository<Quizzes>
    {
        Task<Quizzes?> UpdateAsync(Guid id, Quizzes model);
        IQueryable<Quizzes> GetDoneQuiz(string userId);
        IQueryable<Quizzes> GetFilter(bool status = true, bool isDescending = true);
    }
}
