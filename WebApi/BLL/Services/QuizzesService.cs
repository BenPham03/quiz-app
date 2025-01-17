using BLL.Services.Base;
using BLL.ViewModels;
using DAL.Infratructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class QuizzesService : BaseService<Quizzes>
    {
        public QuizzesService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public async Task<Quizzes?> GetByIdAsync(Guid id)
        {
            // Sử dụng includeProperties để load Questions và Answers
            return await base.GetByIdAsync(id, includeProperties: "Questions,Questions.Answers");
        }
        public async Task<bool> UpdateQuizAsync(Guid quizId, updateQuizzesVM updateQuizzesVM)
        {
            var quiz = await _unitOfWork.Quizzes.GetByIdAsync(quizId);
            if (quiz == null)
            {
                throw new KeyNotFoundException("Quiz không tồn tại.");
            }

            // Cập nhật dữ liệu chính của Quiz
            quiz.Title = updateQuizzesVM.Title;
            quiz.Description = updateQuizzesVM.Description;
            quiz.Subject = updateQuizzesVM.Subject;
            quiz.Config = updateQuizzesVM.Config;
            quiz.LastUpdateAt = DateTime.UtcNow;

            // Lấy danh sách câu hỏi hiện tại
            var existingQuestions = quiz.Questions.ToList();

            // Cập nhật hoặc thêm câu hỏi mới
            foreach (var questionVM in updateQuizzesVM.Questions)
            {
                var existingQuestion = existingQuestions.FirstOrDefault(q => q.Id == questionVM.Id);

                if (existingQuestion != null)
                {
                    // Cập nhật câu hỏi hiện có
                    existingQuestion.QuestionContent = questionVM.QuestionContent;
                    existingQuestion.QuestionType = questionVM.QuestionType;

                    // Cập nhật hoặc thêm đáp án
                    var existingAnswers = existingQuestion.Answers.ToList();
                    foreach (var answerVM in questionVM.Answers)
                    {
                        var existingAnswer = existingAnswers.FirstOrDefault(a => a.Id == answerVM.Id);

                        if (existingAnswer != null)
                        {
                            // Cập nhật đáp án hiện có
                            existingAnswer.AnswerContent = answerVM.AnswerContent;
                            existingAnswer.IsCorrect = answerVM.IsCorrect;
                            _unitOfWork.GenericRepository<Answers>().Update(existingAnswer);
                        }
                        else
                        {
                            // Thêm đáp án mới
                            existingQuestion.Answers.Add(new Answers
                            {
                                Id = Guid.NewGuid(),
                                AnswerContent = answerVM.AnswerContent,
                                IsCorrect = answerVM.IsCorrect,
                                QuestionId = existingQuestion.Id
                            });
                        }
                    }

                    // Xóa đáp án không còn trong request
                    foreach (var answer in existingAnswers)
                    {
                        if (!questionVM.Answers.Any(a => a.Id == answer.Id))
                        {
                            _unitOfWork.GenericRepository<Answers>().Delete(answer);
                        }
                    }

                    _unitOfWork.GenericRepository<Questions>().Update(existingQuestion);
                }
                else
                {
                    // Thêm câu hỏi mới
                    quiz.Questions.Add(new Questions
                    {
                        Id = Guid.NewGuid(),
                        QuestionContent = questionVM.QuestionContent,
                        QuestionType = questionVM.QuestionType,
                        Answers = questionVM.Answers.Select(a => new Answers
                        {
                            Id = Guid.NewGuid(),
                            AnswerContent = a.AnswerContent,
                            IsCorrect = a.IsCorrect
                        }).ToList()
                    });
                }
            }

            // Xóa câu hỏi không còn trong request
            foreach (var question in existingQuestions)
            {
                if (!updateQuizzesVM.Questions.Any(q => q.Id == question.Id))
                {
                    _unitOfWork.GenericRepository<Questions>().Delete(question);
                }
            }

            // Cập nhật Quiz
            _unitOfWork.GenericRepository<Quizzes>().Update(quiz);

            // Lưu thay đổi
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

    }
}
