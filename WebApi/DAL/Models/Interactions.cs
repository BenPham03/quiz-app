namespace DAL.Models
{
    public enum InteractType
    {
        Like,
        Share,
        Save
    }
    public class Interactions
    {
        public Guid Id { get; set; }
        public InteractType Type { get; set; }
        public DateTime CreatedAt { get; set; }= DateTime.Now;

        public string? UserId { get; set; }
        public AppUser? User { get; set; }
        public Guid? QuizzId { get; set; }
        public Quizzes? Quizzes { get; set; }
    }
}
