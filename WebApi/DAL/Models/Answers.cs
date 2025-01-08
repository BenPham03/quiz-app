using System.Text.Json.Serialization;

namespace DAL.Models
{
    public class Answers
    {
        public Guid Id { get; set; }
        public string AnswerContent {  get; set; }
        public bool IsCorrect { get; set; } = false;

        public Guid? QuestionId { get; set; }
        public Questions? Questions { get; set; }
        [JsonIgnore]
        public ICollection<UserAnswers> UserAnswers { get; set; }= new List<UserAnswers>();
    }
}
