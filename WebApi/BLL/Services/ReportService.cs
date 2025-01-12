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

        public async Task<List<Questions>> top5right(Guid examId)
        {
            return await _unitOfWork.Report.top5right(examId);
        }

        public async Task<List<Questions>> top5wrong(Guid examId)
        {
            return await _unitOfWork.Report.top5wrong(examId);
        }

        public async Task<List<RankVM>> Rank(Guid examId)
        {
            var data =await _unitOfWork.Report.rank(examId);
            return data;
        }
        public async Task<List<RankVM>> Analyst(Guid examId)
        {
            var data = await _unitOfWork.Report.analyst(examId);
            return data;
        }
    }
}
