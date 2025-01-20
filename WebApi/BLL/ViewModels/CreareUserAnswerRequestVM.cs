using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class CreareUserAnswerRequestVM
    {
        public Guid? QuestionId { get; set; }
        public Guid? AnswerId { get; set; }
    }
}
