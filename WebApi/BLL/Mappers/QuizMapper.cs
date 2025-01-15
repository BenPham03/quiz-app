using BLL.ViewModels.Quiz;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{
    public static class QuizMapper
    {
        public static Quizzes ToQuizFromCreateVM(this CreateQuizVM model, string userId)
        {
            return new Quizzes
            {
                Title = model.Title,
                Description = model.Description,
                Config = model.Config,
                Status = model.Status,
                CreatedAt = model.CreatedAt,
                LastUpdateAt = model.LastUpdateAt,
                UserId = userId,
            };
        }
        public static Quizzes ToQuizFromUpdate(this UpdateQuizVM model)
        {
            return new Quizzes
            {
                Title = model.Title,
                Description = model.Description,
                Config = model.Config,
                Status = model.Status,
                LastUpdateAt = model.LastUpdateAt,
            };
        }
    }
}
