using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class AnswersVM
    {
        public Guid Id { get; set; }
        public string AnswerContent { get; set; }
        public bool IsCorrect { get; set; }
    }
}
