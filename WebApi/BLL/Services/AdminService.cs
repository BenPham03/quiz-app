using BLL.Services.Base;
using DAL.Infratructure;
using DAL.Models;

namespace BLL.Services
{
    public class AdminService : BaseService<AppUser>
    {
        public AdminService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<List<UserVM>> GetUserList()
        {
            return await _unitOfWork.Admin.GetUserList();
        }
        public async Task<int> GetUserOnlineCount()
        {
            return await _unitOfWork.Admin.GetUserOnlineCount();
        }
        public async Task<int> GetNewUserCount()
        {
            return await _unitOfWork.Admin.GetNewUserCount();
        }
    }
}
