using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class RankVM
    {
        public string Name { get; set; }
        public string Image {  get; set; }
        public DateTime AttemptAt { get; set; }
        public float Score {  get; set; }
        public int Duration {  get; set; }
    }
}
