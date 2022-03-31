using QuizCreation.DataAccess.Abstract;
using QuizCreation.Entities.Concrete;
using QuizCreation.Entities.ViewModel;

namespace QuizCreation.Business.Services.Students
{
    public interface IStudentService 
    {
        PagedResult<StudentViewModel>GetAll(int pageNumber, int pageSize);
        Task<StudentViewModel> AddAsync(StudentViewModel vm);
        IEnumerable<Student> GetAllStudents();
        bool SetGroupIdToStudents(GroupViewModel vm);
        bool SetExamResult(AttuendExamViewModel vm);
        IEnumerable<ResultViewModel> GetAllExamResult(int studentId);
        StudentViewModel GetStudentDetails(int studentId);
        Task<StudentViewModel> UpdateAsync(StudentViewModel vm);
    }
}
