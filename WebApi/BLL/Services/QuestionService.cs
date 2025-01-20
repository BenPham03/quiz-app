using BLL.Mappers;
using BLL.Services.Base;
using BLL.ViewModels.Question;
using DAL.Infratructure;
using DAL.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class QuestionService : BaseService<Questions>
    {
        public QuestionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<List<QuestionVM>> GetByQuizId(Guid quizId)
        {
            var question = await _unitOfWork.Question.GetByQuizIdAsync(quizId);
            return question.Select(q => q.ToQuestionVM()).ToList();
        }
    }
}
