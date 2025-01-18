using BLL.Mappers;
using BLL.Services.Base;
using BLL.ViewModels.Quiz;
using DAL.Infratructure;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class QuizService : BaseService<Quizzes>
    {
        public QuizService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<Quizzes?> UpdateAsync(Guid id, UpdateQuizVM updateQuizVM)
        {
            var quiz = updateQuizVM.ToQuizFromUpdate();
            await _unitOfWork.SaveChangesAsync();
            return await _unitOfWork.Quiz.UpdateAsync(id,quiz);
        }
        public async Task<PaginatedResult<Quizzes>> GetFilter(int pageIndex = 1, int pageSize = 6, bool status = true, bool isDescending = true)
        {
            var query = _unitOfWork.Quiz.GetFilter(status: status, isDescending : isDescending, includeProperties : "Interactions,Questions");
            var check = await query.ToListAsync();
            return await PaginatedResult<Quizzes>.CreateAsync(query, pageIndex, pageSize);
        }
        public async Task<PaginatedResult<Quizzes>> GetDone(string userId, int pageIndex = 1, int pageSize = 6)
        {
            var quiz = _unitOfWork.Quiz.GetDoneQuiz(userId);
            return await PaginatedResult<Quizzes>.CreateAsync(quiz, pageIndex, pageSize);
        }
    }

}
