using Azure;
using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class QuizzesRepository : GenericRepository<Quizzes>
    {
        private readonly DataDbContext _dbContext;
        public QuizzesRepository(DataDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Quizzes>> GetQuizzesAsync(int page, int pageSize)
        {
            // Bước 1: Lấy danh sách quiz với pagination
            var quizzes = await _dbContext.Quizzes
                .OrderBy(q => q.Id) // Đảm bảo có thứ tự trước khi phân trang
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Bước 2: Load thêm các câu hỏi liên quan (Questions) cho mỗi quiz
            foreach (var quiz in quizzes)
            {
                await _dbContext.Entry(quiz)
                    .Collection(q => q.Questions)
                    .LoadAsync();
            }

            return quizzes;
        }



    }
}
