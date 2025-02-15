﻿using BLL.ViewModels.Attempt;
using DAL.Infratructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class DoExamService 
    {
        private readonly IUnitOfWork _unitOfWork;
        public DoExamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> SubmitAsync(List<UserAnswers> userAnswers, Attempts attempt)
        {
            if (userAnswers.Count() == 0)
            {
                attempt.Score = 0;
                _unitOfWork.Attempt.Add(attempt);
                return await _unitOfWork.SaveChangesAsync();
            }
            var score = _unitOfWork.UserAnswer.Scoring(userAnswers);
            attempt.Score = score;
            _unitOfWork.Attempt.Add(attempt);
            await _unitOfWork.SaveChangesAsync();
            userAnswers.ForEach(c => c.AttemptId = attempt.Id);
            _unitOfWork.UserAnswer.AddRange(userAnswers);
            return await _unitOfWork.SaveChangesAsync();
        }
    }
}
