using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.Quiz
{
    public class CreateQuizVM
    {
        [Required]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Title must be from 6 to 50 characters")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Config { get; set; }
        public bool Status { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdateAt { get; set; } = DateTime.Now;
    }
}
