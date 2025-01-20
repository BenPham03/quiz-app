using System.Text.Json.Serialization;

namespace DAL.Models
{
        public class Attempts
        {
                public Guid Id { get; set; }
                public float Score { get; set; }
                public DateTime AttemptAt { get; set; } = DateTime.Now;
                public string? Name { get; set; }
                public int Duration { get; set; }

                public AppUser? User { get; set; }
                public string? UserId { get; set; }

                public Quizzes? Quizzes { get; set; }
                public Guid? QuizzId { get; set; }
                [JsonIgnore]
                public ICollection<UserAnswers> UserAnswers { get; set; } = new List<UserAnswers>();
        }
}
