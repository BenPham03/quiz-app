﻿using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection.Emit;

namespace DAL.Data
{
    public class DataDbContext:IdentityDbContext<AppUser>
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
                .OnDelete(DeleteBehavior.SetNull); // Đặt UserId null khi User bị xóa

            // AppUser → Attempts
            modelBuilder.Entity<AppUser>()
                .HasMany(a => a.Attempts)
                .WithOne(at => at.User)
                .HasForeignKey(at => at.UserId)
                .OnDelete(DeleteBehavior.SetNull); // Đặt UserId null khi User bị xóa

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

            // Questions → UserAnswers
            modelBuilder.Entity<Questions>()
                .HasMany<UserAnswers>(ques => ques.UserAnswers)
                .WithOne(ua => ua.Questions)
                .HasForeignKey(ua => ua.QuestionId)
                .OnDelete(DeleteBehavior.SetNull); 

            // ------------------- Answers Configuration -------------------

            // Answers → UserAnswers
            modelBuilder.Entity<Answers>()
                .HasMany(ans => ans.UserAnswers)
                .WithOne(ua => ua.Answers)
                .HasForeignKey(ua => ua.AnswerId)
                .OnDelete(DeleteBehavior.SetNull); 

            // ------------------- Attempts Configuration -------------------

            // Attempts → UserAnswers
            modelBuilder.Entity<Attempts>()
                .HasMany(at => at.UserAnswers)
                .WithOne(ua => ua.Attempts)
                .HasForeignKey(ua => ua.AttemptId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa UserAnswers khi Attempts bị xóa
        }
    }
}
