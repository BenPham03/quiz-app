using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class QuizzesVM
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Subject { get; set; }
        public string Config { get; set; }
        public bool Status { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdateAt { get; set; } = DateTime.Now;
        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        [JsonIgnore]
        public ICollection<Questions> Questions { get; set; } = new List<Questions>();

        [JsonIgnore]
        public ICollection<Interactions> Interactions { get; set; } = new List<Interactions>();
        [JsonIgnore]
        public ICollection<Attempts> Attempts { get; set; } = new List<Attempts>();
    }
}
