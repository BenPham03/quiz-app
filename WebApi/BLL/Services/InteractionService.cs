using BLL.Services.Base;
using DAL.Infratructure;
using DAL.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class InteractionService : BaseService<Interactions>
    {
        public InteractionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public bool InteractionExist(Guid quizId, string userId)
        {
            return _unitOfWork.Interaction.InteractionExist(quizId, userId);
        }
        public async Task<int?> DeleteByQuizIdUserId(Guid quizId, string userId)
        {
            var interaction = _unitOfWork.Interaction.GetByQuizIdUserId(quizId, userId);
            if (interaction != null)
            {
                _unitOfWork.Interaction.Delete(interaction);
                return await _unitOfWork.SaveChangesAsync();
            }
            return null;
        }
    }
}
