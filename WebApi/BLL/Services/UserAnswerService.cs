﻿using BLL.Services.Base;
using DAL.Infratructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserAnswerService : BaseService<UserAnswers>
    {
        public UserAnswerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
