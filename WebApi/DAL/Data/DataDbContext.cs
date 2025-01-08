using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection.Emit;

namespace DAL.Data
{
    public class DataDbContext : IdentityDbContext<AppUser>
    {
        private readonly IConfiguration _configuration;
        public DbSet<Quizzes> Quizzes { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Answers> Answers { get; set; }
        public DbSet<Interactions> Interactions { get; set; }
        public DbSet<Attempts> Attempts { get; set; }
        public DbSet<UserAnswers> UserAnswers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DataDbContext(DbContextOptions<DataDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnectionString"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ------------------- AppUser Configuration -------------------

            // AppUser → Quizzes
            modelBuilder.Entity<AppUser>()
                .HasMany(a => a.Quizzes)
                .WithOne(q => q.User)
                .HasForeignKey(q => q.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa Quizzes khi User bị xóa

            // AppUser → Interactions
            modelBuilder.Entity<AppUser>()
                .HasMany(a => a.Interactions)
                .WithOne(i => i.User)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.NoAction); // Đặt UserId null khi User bị xóa

            // AppUser → Attempts
            modelBuilder.Entity<AppUser>()
                .HasMany(a => a.Attempts)
                .WithOne(at => at.User)
                .HasForeignKey(at => at.UserId)
                .OnDelete(DeleteBehavior.NoAction); // Đặt UserId null khi User bị xóa

            //// ------------------- Quizzes Configuration -------------------

            // Quizzes → Questions
            modelBuilder.Entity<Quizzes>()
                .HasMany(q => q.Questions)
                .WithOne(ques => ques.Quizzes)
                .HasForeignKey(ques => ques.QuizzId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa Questions khi Quizzes bị xóa

            //// Quizzes → Interactions
            modelBuilder.Entity<Quizzes>()
                .HasMany(q => q.Interactions)
                .WithOne(i => i.Quizzes)
                .HasForeignKey(i => i.QuizzId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa Interactions khi Quizzes bị xóa

            // Quizzes → Attempts
            modelBuilder.Entity<Quizzes>()
                .HasMany(q => q.Attempts)
                .WithOne(at => at.Quizzes)
                .HasForeignKey(at => at.QuizzId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa Attempts khi Quizzes bị xóa

            // ------------------- Questions Configuration -------------------

            // Questions → Answers
            modelBuilder.Entity<Questions>()
                .HasMany(ques => ques.Answers)
                .WithOne(ans => ans.Questions)
                .HasForeignKey(ans => ans.QuestionId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa Answers khi Questions bị xóa

            // ---------------------UserAnswer Cofiguration---------------------

            modelBuilder.Entity<UserAnswers>(ua => ua.HasKey(a => new { a.AnswerId, a.QuestionId, a.AttemptId }));

            // UserAnswers → Attempts  
            modelBuilder.Entity<UserAnswers>()
                .HasOne(ua => ua.Attempts)
                .WithMany(at => at.UserAnswers)
                .HasForeignKey(ua => ua.AttemptId)
                .OnDelete(DeleteBehavior.Cascade);

            // UserAnswers → Questions  
            modelBuilder.Entity<UserAnswers>()
                .HasOne(ua => ua.Questions)
                .WithMany(at => at.UserAnswers)
                .HasForeignKey(ua => ua.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            // UserAnswers → Anwsers  
            modelBuilder.Entity<UserAnswers>()
                .HasOne(ua => ua.Answers)
                .WithMany(at => at.UserAnswers)
                .HasForeignKey(ua => ua.AnswerId)
                .OnDelete(DeleteBehavior.NoAction);


            // ------------------- Enum Configuration -------------------

            // Lưu QuestionType dưới dạng chuỗi
            modelBuilder.Entity<Questions>()
                .Property(ques => ques.QuestionType)
                .HasConversion<string>();

            // Lưu InteractType dưới dạng chuỗi
            modelBuilder.Entity<Interactions>()
                .Property(inter => inter.Type)
                .HasConversion<string>();

            // ------------------- Index Configuration -------------------

            // Unique constraint cho Interactions (UserId, QuizzId)
            modelBuilder.Entity<Interactions>()
                .HasIndex(i => new { i.UserId, i.QuizzId })
                .IsUnique();
        }
    }
}
