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

        public async Task<List<Questions>> top5right(Guid id)
        {
            var quizz = await _dbContext.Quizzes
                        .Include(q => q.Attempts)
                            .ThenInclude(a => a.UserAnswers)
                            .ThenInclude(ua => ua.Questions)
                        .Include(q => q.Attempts)
                            .ThenInclude(a => a.UserAnswers)
                            .ThenInclude(ua => ua.Answers)
                        .FirstOrDefaultAsync(q => q.Id == id);

            if (quizz == null || quizz.Attempts == null || !quizz.Attempts.Any())
            {
                return new List<Questions>();
            }

            var result = quizz.Attempts
                .Where(a => a.UserAnswers != null) // Lọc các Attempts có UserAnswers
                .SelectMany(a => a.UserAnswers)
                .Where(ua => ua.Questions != null && ua.Answers != null) // Lọc câu trả lời có Questions và Answers
                .GroupBy(ua => ua.Questions) // Nhóm theo câu hỏi
                .Where(g => g.Key != null) // Đảm bảo nhóm có câu hỏi hợp lệ
                .Select(g => new
                {
                    Question = g.Key, // Lấy câu hỏi từ nhóm
                    Count = g.Count(ua => ua.Answers.IsCorrect) // Đếm số câu trả lời đúng
                })
                .OrderByDescending(q => q.Count) // Sắp xếp giảm dần theo số câu trả lời đúng
                .Take(5) // Lấy top 5
                .Select(q => q.Question) // Lấy câu hỏi từ kết quả
                .ToList();

            return result;
        }

        public async Task<List<Questions>> top5wrong(Guid id)
        {
            var quizz = await _dbContext.Quizzes
                        .Include(q => q.Attempts)
                            .ThenInclude(a => a.UserAnswers)
                            .ThenInclude(ua => ua.Questions)
                        .Include(q => q.Attempts)
                            .ThenInclude(a => a.UserAnswers)
                            .ThenInclude(ua => ua.Answers)
                        .FirstOrDefaultAsync(q => q.Id == id);

            if (quizz == null || quizz.Attempts == null || !quizz.Attempts.Any())
            {
                return new List<Questions>();
            }

            var result = quizz.Attempts
                .Where(a => a.UserAnswers != null) 
                .SelectMany(a => a.UserAnswers)
                .Where(ua => ua.Questions != null && ua.Answers != null) 
                .GroupBy(ua => ua.Questions) 
                .Where(g => g.Key != null)
                .Select(g => new
                {
                    Question = g.Key, // Lấy câu hỏi từ nhóm
                    Count = g.Count(ua => !ua.Answers.IsCorrect)
                })
                .OrderByDescending(q => q.Count)
                .Take(5) // Lấy top 5
                .Select(q => q.Question) 
                .ToList();

            return result;
        }

        public async Task<List<RankVM>> rank(Guid id)
        {
            var quizz = await _dbContext.Quizzes
                        .Where(q => q.Id == id)
                        .Include(q => q.Attempts)
                            .ThenInclude(u => u.User)
                        .SelectMany(q=>q.Attempts)
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

        public async Task<List<RankVM>> analyst(Guid id)
        {
            var quizz = await _dbContext.Quizzes
                        .Where(q => q.Id == id)
                        .Include(q => q.Attempts)
                            .ThenInclude(u => u.User)
                        .SelectMany(q => q.Attempts)
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
