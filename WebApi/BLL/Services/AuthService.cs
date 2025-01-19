using BLL.Services.Base;
using DAL.Infratructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthService : BaseService<AppUser>
    {
        public AuthService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<int> EditTimeLine(AppUser user, string timeline)
        {
            user.Timeline = "{" + user.Timeline.Trim('{').Trim('}') + "," + timeline +"}";
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
