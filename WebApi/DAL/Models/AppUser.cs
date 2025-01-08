using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class AppUser : IdentityUser
    {
        public string? Image { get; set; }
        public string Timeline { get; set; }
        [JsonIgnore]
        public ICollection<Quizzes> Quizzes { get; set; } = new List<Quizzes>();
        [JsonIgnore]
        public ICollection<Interactions> Interactions { get; set; } = new List<Interactions>();
        [JsonIgnore]
        public ICollection<Attempts> Attempts { get; set; } = new List<Attempts>();
    }
}
