using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AdminRepository : GenericRepository<AppUser>
    {
        private readonly DataDbContext _dbContext;
        public AdminRepository(DataDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<UserVM>> GetUserList()
        {
            var userRole = await _dbContext.Roles
                .Where(r => r.Name == "User")
                .Select(r => r.Id)
                .FirstOrDefaultAsync();

            if (userRole == null)
                return new List<UserVM>();

            var result = await _dbContext.Users
                .Where(u => _dbContext.UserRoles.Any(ur => ur.UserId == u.Id && ur.RoleId == userRole))
                .Select(u => new UserVM
                {
                    Id = Guid.Parse(u.Id),
                    UserName = u.UserName,
                    Gmail = u.Email,
                    TimeLine = u.Timeline,
                    ExamCount = _dbContext.Quizzes.Count(q => q.UserId == u.Id),
                    ExamSaved = _dbContext.Interactions.Count(i => i.UserId == u.Id && i.QuizzId != null && i.Type == InteractType.Save), 
                    ExamLiked = _dbContext.Interactions.Count(i => i.UserId == u.Id && i.QuizzId != null && i.Type == InteractType.Like), 
                    ExamDone = _dbContext.Attempts.Count(a => a.UserId == u.Id)
                })
                .ToListAsync();
            return result;
        }
        public async Task<int> GetUserOnlineCount()
        {
            var onlineUserCount = await _dbContext.RefreshTokens
               .Where(rt => rt.DateExpire > DateTime.UtcNow && !rt.IsRevoked)
               .Select(rt => rt.UserId)
               .Distinct()
               .CountAsync();

            return onlineUserCount;
        }
        public async Task<int> GetNewUserCount()
        {
            var today = DateTime.UtcNow.Date; // Lấy ngày hôm nay (UTC)
            var tomorrow = today.AddDays(1); // Lấy ngày mai để so sánh

            var users = await _dbContext.Users
                .Where(u => u.CreatedDate >= today && u.CreatedDate < tomorrow)
                .CountAsync();

            return users;
        }
    }
}
