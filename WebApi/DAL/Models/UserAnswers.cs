namespace DAL.Models
{
    public class UserAnswers
    {
        public Guid? AttemptId { get; set; }
        public Attempts? Attempts { get; set; }
        public Guid? QuestionId { get; set; }
        public Questions? Questions { get; set; }
        public Guid? AnswerId { get; set; }
        public Answers? Answers { get; set; }
    }
}
