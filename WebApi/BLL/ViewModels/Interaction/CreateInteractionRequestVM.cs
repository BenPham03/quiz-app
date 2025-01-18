using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.Interaction
{
    public class CreateInteractionRequestVM
    {
        public InteractType Type { get; set; }
        public Guid QuizzId { get; set; }
    }
}
