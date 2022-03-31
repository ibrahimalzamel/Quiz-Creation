using Microsoft.Extensions.Logging;
using QuizCreation.DataAccess.Abstract;
using QuizCreation.Entities.Concrete;
using QuizCreation.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Business.Services.Exams
{
    public class ExamManager : IExamService
    {
        IExamDal _examDal;
        ILogger<ExamManager> _ilogger;

        public ExamManager(IExamDal examDal, ILogger<ExamManager> ilogger)
        {
            _examDal = examDal;
            _ilogger = ilogger;
        }

        public async Task<ExamViewModel> AddAsync(ExamViewModel examVM)
        {
            try
            {
                Exam objExam = examVM.ConvertExamViewModel(examVM);
                await _examDal.AddAsync(objExam);

            }
            catch (Exception ex)
            {

               return null;
            }
            return examVM;
        }

        public PagedResult<ExamViewModel> GetAll(int pageNumber, int pageSize)
        {
            var model = new ExamViewModel();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                List<ExamViewModel> detailList = new List<ExamViewModel>();
                var modelList = _examDal.GetAll().Skip(ExcludeRecords)
                    .Take(pageSize).ToList();
                var totalCount = _examDal.GetAll().ToList();
                detailList = ExamListInfo(modelList);
                if (detailList != null)
                {
                    model.ExamList = detailList;
                    model.TotalCount = totalCount.Count;
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
            }
            var result = new PagedResult<ExamViewModel>
            {
                Data = model.ExamList,
                TotalItems = model.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
            return result;
        }

        private List<ExamViewModel> ExamListInfo(List<Exam> modelList)
        {
           return modelList.Select(x => new ExamViewModel(x)).ToList();
        }

        public IEnumerable<Exam> GetAllExams()
        {
            try
            {
                var exams = _examDal.GetAll();
                return exams;

            }
            catch (Exception ex)
            {

                _ilogger.LogError(ex.Message);
            }
            return Enumerable.Empty<Exam>();
        }
    }
}
