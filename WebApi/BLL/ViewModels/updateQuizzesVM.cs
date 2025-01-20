using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class updateQuizzesVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string Config { get; set; }
        public List<QuestionsVM> Questions { get; set; }
    }
}
