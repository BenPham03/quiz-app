using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // Seed Roles
            var adminRoleId = Guid.NewGuid().ToString();
            var userRoleId = Guid.NewGuid().ToString();
            
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = userRoleId, Name = "User", NormalizedName = "USER" }
            );
            
            // Seed AppUser data
            var user1Id = Guid.NewGuid().ToString();
            var user2Id = Guid.NewGuid().ToString();
            var admin1Id = Guid.NewGuid().ToString();
            
            var user1 = new AppUser
            {
                Id = user1Id,
                UserName = "user1",
                Email = "user1@example.com",
                NormalizedUserName = "USER1",
                NormalizedEmail = "USER1@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "User@123")
            };
            
            var user2 = new AppUser
            {
                Id = user2Id,
                UserName = "user2",
                Email = "user2@example.com",
                NormalizedUserName = "USER2",
                NormalizedEmail = "USER2@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "User@123")
            };
            
            var admin1 = new AppUser
            {
                Id = admin1Id,
                UserName = "admin1",
                Email = "admin1@example.com",
                NormalizedUserName = "ADMIN1",
                NormalizedEmail = "ADMIN1@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "Admin@123")
            };
            
            modelBuilder.Entity<AppUser>().HasData(user1, admin1);
            
            // Link Users to Roles
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = user1Id, RoleId = userRoleId },
                new IdentityUserRole<string> { UserId = admin1Id, RoleId = adminRoleId }
            );
            
            // Seed Quizzes data
            var quiz1Id = Guid.NewGuid();
            var quiz2Id = Guid.NewGuid();
            var quiz3Id = Guid.NewGuid();
            
            modelBuilder.Entity<Quizzes>().HasData(
                new Quizzes { Id = quiz1Id, Title = "Quiz 1", UserId = user1Id, Config = "{'time':3600}" },
                new Quizzes { Id = quiz2Id, Title = "Quiz 2", UserId = user1Id,Status=true,Config="{'time':3600}",Description="" },
                new Quizzes { Id = quiz3Id, Title = "Quiz 3", UserId = user1Id, Config = "{'time':3600}" }
            );
            
            // Seed Questions data
            var question1Id = Guid.NewGuid();
            var question2Id = Guid.NewGuid();
            var question3Id = Guid.NewGuid();
            
            modelBuilder.Entity<Questions>().HasData(
                new Questions { Id = question1Id, QuestionContent = "What is 'Cai gi'?", QuizzId = quiz1Id,QuestionType=QuestionType.Choice },
                new Questions { Id = question2Id, QuestionContent = "What la cai gi?", QuizzId = quiz1Id ,QuestionType=QuestionType.Choice},
                new Questions { Id = question3Id, QuestionContent = "Cai gi la 'What'?", QuizzId = quiz1Id,QuestionType=QuestionType.MultipleChoice }
            );
            
            // Seed Answers data
            var answer1Id = Guid.NewGuid();
            var answer2Id = Guid.NewGuid();
            var answer3Id = Guid.NewGuid();
            var answer4Id = Guid.NewGuid();
            var answer5Id = Guid.NewGuid();
            var answer6Id = Guid.NewGuid();
            var answer7Id = Guid.NewGuid();
            var answer8Id = Guid.NewGuid();
            var answer9Id = Guid.NewGuid();
            
            modelBuilder.Entity<Answers>().HasData(
                new Answers { Id = answer1Id, AnswerContent = "No", QuestionId = question1Id,IsCorrect=true },
                new Answers { Id = answer2Id, AnswerContent = "know", QuestionId = question1Id },
                new Answers { Id = answer3Id, AnswerContent = "What", QuestionId = question1Id },
                new Answers { Id = answer4Id, AnswerContent = "No", QuestionId = question2Id, IsCorrect = true },
                new Answers { Id = answer5Id, AnswerContent = "know", QuestionId = question2Id },
                new Answers { Id = answer6Id, AnswerContent = "What", QuestionId = question2Id },
                new Answers { Id = answer7Id, AnswerContent = "No", QuestionId = question3Id, IsCorrect = true },
                new Answers { Id = answer8Id, AnswerContent = "know", QuestionId = question3Id ,IsCorrect=true},
                new Answers { Id = answer9Id, AnswerContent = "What", QuestionId = question3Id }
            );
            
            // Seed Interactions data
            modelBuilder.Entity<Interactions>().HasData(
                new Interactions { Id = Guid.NewGuid(), UserId = user2Id, QuizzId = quiz1Id },
                new Interactions { Id = Guid.NewGuid(), UserId = user2Id, QuizzId = quiz2Id }
            );
            
            // Seed Attempts data
            var attempt1Id = Guid.NewGuid();
            var attempt2Id = Guid.NewGuid();
            var attempt3Id = Guid.NewGuid();
            
            modelBuilder.Entity<Attempts>().HasData(
                new Attempts { Id = attempt1Id, UserId = user1Id, QuizzId = quiz1Id },
                new Attempts { Id = attempt2Id, UserId = user2Id, QuizzId = quiz1Id },
                new Attempts { Id = attempt3Id, UserId = user2Id, QuizzId = quiz1Id }
            );
            
            // Seed UserAnswers data
            modelBuilder.Entity<UserAnswers>().HasData(
                new UserAnswers {  AnswerId = answer1Id, QuestionId = question1Id, AttemptId = attempt1Id },
                new UserAnswers {  AnswerId = answer4Id, QuestionId = question2Id, AttemptId = attempt1Id },
                new UserAnswers {  AnswerId = answer7Id, QuestionId = question3Id, AttemptId = attempt1Id },
                new UserAnswers { AnswerId = answer2Id, QuestionId = question1Id, AttemptId = attempt2Id },
                new UserAnswers {  AnswerId = answer5Id, QuestionId = question2Id, AttemptId = attempt2Id },
                new UserAnswers {  AnswerId = answer8Id, QuestionId = question3Id, AttemptId = attempt2Id },
                new UserAnswers { AnswerId = answer3Id, QuestionId = question1Id, AttemptId = attempt3Id },
                new UserAnswers { AnswerId = answer6Id, QuestionId = question2Id, AttemptId = attempt3Id },
                new UserAnswers { AnswerId = answer9Id, QuestionId = question3Id, AttemptId = attempt3Id }
            );
        } 
    } 
}
