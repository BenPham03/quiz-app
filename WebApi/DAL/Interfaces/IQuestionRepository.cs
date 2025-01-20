﻿using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IQuestionRepository : IGenericRepository<Questions>
    {
        Task<List<Questions>> GetByQuizIdAsync(Guid quizId);
    }
}
