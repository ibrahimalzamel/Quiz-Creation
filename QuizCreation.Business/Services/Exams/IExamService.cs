using QuizCreation.Entities.Concrete;
using QuizCreation.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Business.Services.Exams
{
    public interface IExamService
    {
        PagedResult<ExamViewModel> GetAll(int pageNumber, int pageSize);
        Task<ExamViewModel>AddAsync(ExamViewModel examVM);
        IEnumerable<Exam> GetAllExams();
    }
}
