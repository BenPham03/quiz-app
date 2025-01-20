using BLL.ViewModels.Question;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class QuestionMapper
    {
        public static QuestionVM ToQuestionVM(this Questions model)
        {
            return new QuestionVM
            {
                Id = model.Id,
                QuestionContent = model.QuestionContent,
                QuestionType = model.QuestionType,
                CreatedAt = model.CreatedAt,
                UpdatedAt = model.UpdatedAt,
                QuizzId = model.QuizzId,
                Answers = model.Answers,
            };
        }
    }
}
