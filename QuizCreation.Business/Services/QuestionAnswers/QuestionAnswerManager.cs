using Microsoft.Extensions.Logging;
using QuizCreation.DataAccess.Abstract;
using QuizCreation.Entities.Concrete;
using QuizCreation.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Business.Services.QuestionAnswers
{
    public class QuestionAnswerManager : IQuestionAnswerService
    {
        IQuestionAnswerDal _answerDal;
        ILogger<QuestionAnswerManager> _ilogger;
        IExamResultDal _examResultDal;
        public QuestionAnswerManager(IQuestionAnswerDal answerDal, ILogger<QuestionAnswerManager> ilogger, IExamResultDal examResultDal)
        {
            _answerDal = answerDal;
            _ilogger = ilogger;
            _examResultDal = examResultDal;
        }

        public async Task<QuestionAnswerViewModel> AddAsync(QuestionAnswerViewModel questionAnswerVM)
        {
            try
            {
                QuestionAnswer objQuestionAnswer = questionAnswerVM.ConvertViewModel(questionAnswerVM);
                await _answerDal.AddAsync(objQuestionAnswer);
            }
            catch (Exception ex)
            {

                return null;
            }
            return questionAnswerVM;
        }

        public PagedResult<QuestionAnswerViewModel> GetAll(int pageNumber, int pageSize)
        {
            var model = new QuestionAnswerViewModel();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                List<QuestionAnswerViewModel> detailList = new List<QuestionAnswerViewModel>();
                var modelList = _answerDal.GetAll().Skip(ExcludeRecords)
                    .Take(pageSize).ToList();
                var totalCount = _answerDal.GetAll().ToList();
                detailList = QuestionAnswerListInfo(modelList);
                if (detailList != null)
                {
                    model.QuestionAnswerList = detailList;
                    model.TotalCount = totalCount.Count;
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
            }
            var result = new PagedResult<QuestionAnswerViewModel>
            {
                Data = model.QuestionAnswerList,
                TotalItems = model.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
            return result;
        }

        private List<QuestionAnswerViewModel> QuestionAnswerListInfo(List<QuestionAnswer> modelList)
        {
            return modelList.Select(x => new QuestionAnswerViewModel(x)).ToList();  
        }

        public IEnumerable<QuestionAnswerViewModel> GetAllQuestionAnswersByExamId(int examId)
        {
            try
            {
                var qnaList =_answerDal.GetAll()
                    .Where(x=>x.ExamId==examId);
                return QuestionAnswerListInfo (qnaList.ToList());
            }
            catch (Exception ex)
            {

              _ilogger.LogError (ex.Message);
            }
            return Enumerable.Empty<QuestionAnswerViewModel>();
        }

        public bool IsExamAttended(int examId, int studentId)
        {
            try
            {
                var qnaRecord = _examResultDal.GetAll()
                    .FirstOrDefault(x=>x.ExamId==examId&&x.StudentId==studentId);
                return qnaRecord==null ? false:true;
            }
            catch (Exception ex)
            {

                _ilogger.LogError(ex.Message);
            }
            return false;
        }
    }
}
