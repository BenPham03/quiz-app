using DAL.Data;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class AnswerRepository : GenericRepository<Answers>, IAnswerRepository
    {
        public AnswerRepository(DataDbContext dbContext) : base(dbContext)
        {
        }
    }
}
