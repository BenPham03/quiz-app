using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class UpdatePasswordVM
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
