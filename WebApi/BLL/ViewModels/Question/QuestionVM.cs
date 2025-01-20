using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.Question
{

    public class QuestionVM
    {

            public Guid Id { get; set; }
            public string QuestionContent { get; set; }
            public QuestionType QuestionType { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.Now;
            public DateTime UpdatedAt { get; set; } = DateTime.Now;

            public Guid? QuizzId { get; set; }
            public Quizzes? Quizzes { get; set; }
            public ICollection<Answers> Answers { get; set; } = new List<Answers>();
    }
}
