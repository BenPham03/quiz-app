using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.Attempt
{
    public class CreateAttemptRequest
    {
        public float Score { get; set; }
        public DateTime AttemptAt { get; set; } = DateTime.Now;
        public string? Name { get; set; }
        public int Duration { get; set; }
        public Guid? QuizzId { get; set; }
    }
}
