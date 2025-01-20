using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAttemptRepository : IGenericRepository<Attempts>
    {
        Task<List<Attempts>> GetMostRecent(string userId);
    }
}
