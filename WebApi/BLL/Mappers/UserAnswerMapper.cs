using BLL.ViewModels;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class UserAnswerMapper 
    {
        public static UserAnswers ToUserAnswerFromCreate(this CreareUserAnswerRequestVM model)
        {
            return new UserAnswers
            { 
                AnswerId = model.AnswerId,
                QuestionId = model.QuestionId,
            };
        }
    }
}
