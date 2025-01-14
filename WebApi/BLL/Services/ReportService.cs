using BLL.Services.Base;
using BLL.ViewModels;
using DAL.Infratructure;
using DAL.Models;
using DAL.Repositories;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace BLL.Services
{
    public class ReportService : BaseService<Quizzes>
    {
        public ReportService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<Quizzes>> GetQuizzes()
        {
            return await _unitOfWork.Report.QuizzList();
        }

        public async Task<List<Questions>> top5right(Guid examId,DateTime from,DateTime to)
        {
            return await _unitOfWork.Report.top5right(examId,from,to);
        }

        public async Task<List<Questions>> top5wrong(Guid examId, DateTime from, DateTime to)
        {
            return await _unitOfWork.Report.top5wrong(examId, from, to);
        }

        public async Task<List<RankVM>> Rank(Guid examId, DateTime from, DateTime to)
        {
            var data =await _unitOfWork.Report.rank(examId, from, to);
            return data;
        }
        public async Task<List<RankVM>> Analyst(Guid examId, DateTime from, DateTime to)
        {
            var data = await _unitOfWork.Report.analyst(examId, from, to);
            return data;
        }
    }
}
