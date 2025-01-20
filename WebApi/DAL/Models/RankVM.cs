namespace DAL.Models
{
    public class RankVM
    {
        public string UserName { get; set; }
        public string Image {  get; set; }
        public DateTime AttemptAt { get; set; }
        public float Score {  get; set; }
        public int Duration {  get; set; }
    }
}
