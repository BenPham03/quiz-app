using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class addQuizzesVM
    {
        [Required(ErrorMessage ="Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Config is required")]
        public string Config { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Question is required")]
        public Questions[] Questions { get; set; }
        public bool Status { get; set; }
    }
}
