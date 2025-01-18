using BLL.ViewModels.Attempt;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class SubmitExamRequestVM
    {
        public CreateAttemptRequest Attempt { get; set; }
        public List<CreareUserAnswerRequestVM> UserAnswers { get; set; }
    }
}
