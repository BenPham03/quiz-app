using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{

    public class QuestionsVM
    {
        public Guid Id { get; set; }
        public string QuestionContent { get; set; }
        public QuestionType QuestionType { get; set; } // Dùng enum để map kiểu câu hỏi
        public List<AnswersVM> Answers { get; set; }
    }
}
