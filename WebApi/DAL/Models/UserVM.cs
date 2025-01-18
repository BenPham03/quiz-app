namespace DAL.Models
{
    public class UserVM
    {
        public Guid Id { get; set; }
        public string UserName {  get; set; }
        public string Image { get; set; }
        public string Gmail {  get; set; }
        public string TimeLine {  get; set; }
        public int ExamCount { get; set; }
        public int ExamSaved {  get; set; }
        public int ExamLiked {  get; set; }
        public int ExamDone {  get; set; }
    }
}
