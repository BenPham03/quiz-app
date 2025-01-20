using BLL.Mappers;
using BLL.Services.Base;
using BLL.ViewModels.Attempt;
using DAL.Infratructure;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AttemptService : BaseService<Attempts>
    {
        public AttemptService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<List<AttemptsVM>> GetMostRecent(string userId , int pageIndex = 1, int pageSize = 6)
        {
            var attempts = await _unitOfWork.Attempt.GetMostRecent(userId);
            return attempts.Select( c => c.ToAttemptVM()).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
