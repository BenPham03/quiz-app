using System.Text.Json.Serialization;

namespace DAL.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum QuestionType
    {
        Choice,
        MultipleChoice
    }
    public class Questions
    {
        public Guid Id { get; set; }
        public string QuestionContent {  get; set; }
        public QuestionType QuestionType {  get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public DateTime UpdatedAt {  get; set; }= DateTime.Now;
        public List<Answers> Answers { get; set; } = new List<Answers>();
        [JsonIgnore]
        public Guid? QuizzId { get; set; }
        public Quizzes? Quizzes { get; set; }
        [JsonIgnore]
        public List<UserAnswers> UserAnswers { get; set; } = new List<UserAnswers>();

    }
}
