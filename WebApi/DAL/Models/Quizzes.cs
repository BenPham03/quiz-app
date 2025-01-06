using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class Quizzes
    {
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Config {  get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public DateTime LastUpdateAt {  get; set; }= DateTime.Now;


        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        [JsonIgnore]
        public ICollection<Questions> Questions { get; set; }= new List<Questions>();
        [JsonIgnore]
        public ICollection<Interactions> Interactions { get; set; } = new List<Interactions>();
        [JsonIgnore]
        public ICollection<Attempts> Attempts { get; set; }=new List<Attempts>();
    }
}
