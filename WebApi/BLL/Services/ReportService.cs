using BLL.Services.Base;
using BLL.ViewModels;
using DAL.Infratructure;
using DAL.Models;
using DAL.Repositories;

namespace BLL.Services
{
    public class ReportService : BaseService<Quizzes>
    {
        public ReportService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public List<Questions> top5right(Guid examId)
        {
            return _unitOfWork.Report.top5right(examId);
        }

        public List<Questions> top5wrong(Guid examId)
        {
            return _unitOfWork.Report.top5wrong(examId);
        }

        public List<RankVM> Rank(Guid examId)
        {
            var data = _unitOfWork.Report.rank(examId);
            var result=new List<RankVM>();
            foreach (var item in data) 
            {
                RankVM newItem=new RankVM() 
                { 
                    Name=((dynamic)item).User.UserName,
                    Image= ((dynamic)item).User.Image,
                    AttemptAt= ((dynamic)item).Attempt.AttemptAt,
                    Duration= ((dynamic)item).Attempt.Duration,
                    Score= ((dynamic)item).Attempt.Score,
                };
                result.Add(newItem);
            }
            return result;
        }
        public List<RankVM> Analyst(Guid examId)
        {
            var data = _unitOfWork.Report.rank(examId);
            var result = new List<RankVM>();
            foreach (var item in data)
            {
                RankVM newItem = new RankVM()
                {
                    Name = ((dynamic)item).User.UserName,
                    Image = ((dynamic)item).User.Image,
                    AttemptAt = ((dynamic)item).Attempt.AttemptAt,
                    Duration = ((dynamic)item).Attempt.Duration,
                    Score = ((dynamic)item).Attempt.Score,
                };
                result.Add(newItem);
            }
            return result;
        }
    }
}
