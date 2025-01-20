using BLL.ViewModels.Attempt;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace BLL.Mappers
{
    public static class AttemptMapper
    {
        public static Attempts ToAttemptFromCreate(this CreateAttemptRequest request, string userId = null)
        {
            return new Attempts
            {
                Id = new Guid(),
                Score = request.Score,
                AttemptAt = request.AttemptAt,
                Name = request.Name,
                Duration = request.Duration,
                UserId = userId,
                QuizzId = request.QuizzId,
            };
        }
        public static AttemptsVM ToAttemptVM(this Attempts request)
        {
            return new AttemptsVM
            {
                Score = request.Score,
                AttemptAt = request.AttemptAt,
                Name = request.Name,
                Duration = request.Duration,
                QuizzId = request.QuizzId,
                Quizzes = request.Quizzes
            };
        }
    }
}
