using DAL.Data;
using DAL.Models;
namespace DAL.Repositories
{
    public class ReportRepository:GenericRepository<Quizzes>
    {
        private readonly DataDbContext _dbContext;

        public ReportRepository(DataDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Questions> top5right(Guid id)
        {
            var quiz = GetById(id);
            if (quiz == null || quiz.Questions == null)
            {
                return new List<Questions>(); // Trả về danh sách rỗng nếu quiz hoặc Questions là null
            }
            var result = quiz
                .Questions
                .Select(q=>new
                {
                    Question=q,
                    Count=q.UserAnswers.Count(a=>a.Answers.IsCorrect)
                })
                .OrderByDescending(q=>q.Count)
                .Select(q=>q.Question)
                .Take(5)
                .ToList();
            return result;
        }

        public List<Questions> top5wrong(Guid id)
        {
            var quiz = GetById(id);
            if (quiz == null || quiz.Questions == null)
            {
                return new List<Questions>(); // Trả về danh sách rỗng nếu quiz hoặc Questions là null
            }
            var result = quiz
                .Questions
                .Select(q=>new
                {
                    Question=q,
                    Count=q.UserAnswers.Count(a=>!a.Answers.IsCorrect)
                })
                .OrderByDescending (q=>q.Count)
                .Select(q=>q.Question)
                .Take(5)
                .ToList();
            return result;
        }

        public List<dynamic> rank(Guid id)
        {
            var quiz = GetById(id);
            if (quiz == null || quiz.Questions == null)
            {
                return new List<dynamic>(); // Trả về danh sách rỗng nếu quiz hoặc Questions là null
            }
            var result = quiz
                .Attempts
                .Select(q => new
                {
                    User=q.User,
                    Attempt=q,
                })
                .OrderByDescending(q => q.Attempt.Score)
                .ToList<dynamic>();
            return result;
        }

        public List<dynamic> analyst(Guid id)
        {
            var quiz = GetById(id);
            if (quiz == null || quiz.Questions == null)
            {
                return new List<dy>(); // Trả về danh sách rỗng nếu quiz hoặc Questions là null
            }
            var result = quiz
                .Attempts
                .Select(q => new
                {
                    User = q.User,
                    Attempt = q,
                })
                .ToList<dynamic>();
            return result;
        }
    }
}
