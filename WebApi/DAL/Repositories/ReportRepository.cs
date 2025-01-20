using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
namespace DAL.Repositories
{
    public class ReportRepository:GenericRepository<Quizzes>
    {
        private readonly DataDbContext _dbContext;

        public ReportRepository(DataDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Quizzes>> QuizzList() 
        {
            return await _dbContext.Quizzes.ToListAsync();
        }

        public async Task<List<Questions>> top5right(Guid id,DateTime? from=null,DateTime? to=null)
        {
            // Nếu `from` hoặc `to` không có, thiết lập giá trị mặc định
            to = to ==DateTime.MinValue ? DateTime.UtcNow : to;
            Console.WriteLine(from + " " + to);
            // Truy vấn trực tiếp từ database để tối ưu hóa
            var result = await _dbContext.UserAnswers
                .Where(ua => ua.Attempts.QuizzId == id // Lọc theo Quiz Id
                            && ua.Attempts.AttemptAt >= from // Lọc thời gian bắt đầu (mặc định MinValue nếu không có)
                            && ua.Attempts.AttemptAt <= to // Lọc thời gian kết thúc (mặc định MaxValue nếu không có)
                            )
                .GroupBy(ua => ua.Questions) // Nhóm theo câu hỏi
                .Select(g => new
                {
                    Question = g.Key, // Câu hỏi
                    CorrectCount =g.Count(ua=>ua.Answers.IsCorrect)  // Số lượng câu trả lời đúng
                })
                .OrderByDescending(q => q.CorrectCount) // Sắp xếp giảm dần theo số lượng đúng
                .Take(5) // Lấy top 5
                .Select(q => q.Question) // Chỉ lấy câu hỏi
                .ToListAsync();

            return result;
        }

        public async Task<List<Questions>> top5wrong(Guid id, DateTime? from = null, DateTime? to = null)
        {
            to = to == DateTime.MinValue ? DateTime.UtcNow : to;
            Console.WriteLine(from + " " + to);
            var result = await _dbContext.UserAnswers
                .Where(ua => ua.Attempts.QuizzId == id 
                            && ua.Attempts.AttemptAt >= from 
                            && ua.Attempts.AttemptAt <= to 
                            )
                .GroupBy(ua => ua.Questions) 
                .Select(g => new
                {
                    Question = g.Key, // Câu hỏi
                    CorrectCount = g.Count(ua => ua.Answers.IsCorrect) 
                })
                .OrderByDescending(q => q.CorrectCount) 
                .Take(5) // Lấy top 5
                .Select(q => q.Question)
                .ToListAsync();

            return result;
        }

        public async Task<List<RankVM>> rank(Guid id, DateTime? from = null, DateTime? to = null)
        {
            to = to == DateTime.MinValue ? DateTime.UtcNow : to;
            var quizz = await _dbContext.Quizzes
                        .Where(q => q.Id == id)
                        .Include(q => q.Attempts)
                            .ThenInclude(u => u.User)
                        .SelectMany(q=>q.Attempts)
                        .Where(q=>q.AttemptAt>=from && q.AttemptAt<=to)
                        .Select(i => new RankVM
                        {
                            Image = i.User != null && i.User.Image != null ? i.User.Image: "DefaultImage.png",
                            UserName = i.User != null && i.User.UserName != null ? i.User.UserName : i.Name+" **",
                            AttemptAt=i.AttemptAt,
                            Score=i.Score,
                            Duration=i.Duration
                        })
                        .OrderByDescending(i=>i.Score)
                        .ToListAsync();
            return quizz;
        }

        public async Task<List<RankVM>> analyst(Guid id, DateTime? from = null, DateTime? to = null)
        {
            to = to == DateTime.MinValue ? DateTime.UtcNow : to;
            var quizz = await _dbContext.Quizzes
                        .Where(q => q.Id == id)
                        .Include(q => q.Attempts)
                            .ThenInclude(u => u.User)
                        .SelectMany(q => q.Attempts)
                        .Where(q => q.AttemptAt >= from && q.AttemptAt <= to)
                        .Select(i => new RankVM
                        {
                            Image = i.User != null && i.User.Image != null ? i.User.Image : "DefaultImage.png",
                            UserName = i.User != null && i.User.UserName != null ? i.User.UserName : i.Name + " **",
                            AttemptAt = i.AttemptAt,
                            Score = i.Score,
                            Duration = i.Duration
                        })
                        .ToListAsync();
            return quizz;
        }
    }
}
