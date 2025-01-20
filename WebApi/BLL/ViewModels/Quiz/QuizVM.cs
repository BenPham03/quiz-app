using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.Quiz
{
    public class QuizVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Config { get; set; }
        public bool Status { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdateAt { get; set; } = DateTime.Now;
        public string? UserId { get; set; }
        public ICollection<Questions> Questions { get; set; } = new List<Questions>();
        public ICollection<Interactions> Interactions { get; set; } = new List<Interactions>();
        public ICollection<Attempts> Attempts { get; set; } = new List<Attempts>();
    }
}
