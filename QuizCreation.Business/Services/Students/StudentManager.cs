using Microsoft.Extensions.Logging;
using QuizCreation.Business.Services.Students;
using QuizCreation.Business.ViewModel;
using QuizCreation.DataAccess.Abstract;
using QuizCreation.Entities.Concrete;
using QuizCreation.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Business.Services.Students
{
    public class StudentManager : IStudentService
    {
        IStudentDal StudentDal;
        ILogger<StudentManager> _ilogger;
        IExamResultDal ExamResultDal;
        IQuestionAnswerDal QuestionAnswerDal;
        public StudentManager(IQuestionAnswerDal questionAnswerDal, IExamResultDal  examResultDal, IStudentDal studentDal, ILogger<StudentManager> ilogger)
        {
            StudentDal = studentDal;
            _ilogger = ilogger;
            ExamResultDal = examResultDal;
            QuestionAnswerDal = questionAnswerDal;
        }

        public async Task<StudentViewModel> AddAsync(StudentViewModel vm)
        {
            try
            {
                Student obj = vm.ConvertViewModel(vm);
                await StudentDal.AddAsync(obj);
            }
            catch (Exception ex)
            {

                return null;
            }
         return vm;
        }

        public PagedResult<StudentViewModel> GetAll(int pageNumber, int pageSize)
        {
            var model = new StudentViewModel();
            try
            {
                int ExcludeRecode = (pageSize * pageNumber) - pageSize;
                List<StudentViewModel> detaiList = new List<StudentViewModel>(); 
                var modelList = StudentDal.GetAll()
                    .Skip(ExcludeRecode).Take(pageSize).ToList();
               var totalCount = StudentDal.GetAll().ToList();
                detaiList = GroupListInfo(modelList);
                if (detaiList!=null)
                {
                    model.StudentList = detaiList;
                    model.TotalCount = totalCount.Count();
                }
            }
            catch (Exception ex)
            {

                _ilogger.LogError(ex.Message);
            }
            var result = new PagedResult<StudentViewModel>
            {
                Data = model.StudentList,
                TotalItems = model.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };
            return result;
        }

        private List<StudentViewModel> GroupListInfo(List<Student> modelList)
        {
           return modelList.Select(o=>new StudentViewModel(o)).ToList();    
        }

        public IEnumerable<ResultViewModel> GetAllExamResult(int studentId)
        {
            try
            {
                var examResults = ExamResultDal.GetAll().Where(a => a.StudentId == studentId);
                var students = StudentDal.GetAll();
                var exams = ExamResultDal.GetAll();
                var qnas = QuestionAnswerDal.GetAll();

                var requiredData = examResults.Join(students, er => er.StudentId, s => s.Id,
                    (er, st) => new { er, st })
                    .Join(exams, erj => erj.er.ExamId, ex => ex.Id,(erj, ex) => new { erj, ex })
                    .Join(qnas, exj => exj.erj.er.QuestionAnswerId, q => q.Id,
                    (exj, q) => new ResultViewModel()
                    {
                        StudentId = studentId,
                        ExamName = exj.ex.Exam.Title,
                        TotalQuestion=examResults.Count(a=>a.StudentId==studentId&&a.ExamId==exj.ex.Id),
                        CorrectAnswer =examResults.Count(a=>a.StudentId==studentId&&a.ExamId==exj.ex.Id&&a.Answer==q.Answer),
                        WrongAnswer=examResults.Count(a=>a.StudentId==studentId&&a.ExamId==exj.ex.Id&&a.Answer!=q.Answer)
                    });
                return requiredData;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
            }
            return Enumerable.Empty<ResultViewModel>();
        }

        public IEnumerable<Student> GetAllStudents()
        {
            try
            {
                var students = StudentDal.GetAll();
                return students;
            }
            catch (Exception ex )
            {

                _ilogger.LogError(ex.Message);
            }
            return Enumerable.Empty<Student>();
        }

        public StudentViewModel GetStudentDetails(int studentId)
        {
            try
            {
                var student = StudentDal.GetById(studentId);
                return student !=null ? new StudentViewModel(student) : null;
            }
            catch (Exception ex)
            {

                _ilogger.LogError(ex.Message);
            }
            return null;
        }

        public bool SetExamResult(AttuendExamViewModel vm)
        {
            try
            {
                foreach (var item in vm.questionAnswerViewModels)
                {
                    ExamResult examResult = new ExamResult();
                    examResult.StudentId = vm.StudentId;
                    examResult.QuestionAnswerId = item.Id;
                    examResult.ExamId=item.ExamId;
                    examResult.Answer = item.SelectedAnswer;
                    ExamResultDal.AddAsync(examResult);
                }
                return true;
            }
            catch (Exception ex)
            {

                _ilogger.LogError(ex.Message);
            }
            return false;
        }

        public bool SetGroupIdToStudents(GroupViewModel vm)
        {
            try
            {
                foreach (var item in vm.StudentCheckList)
                {
                    var student = StudentDal.GetById(item.Id);
                    if (item.Selected)
                    {
                        student.GroupId = vm.Id;  
                        StudentDal.Update(student);
                    }
                    else
                    {
                        if(student.GroupId==vm.Id)
                        {
                            student.GroupId=null;
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
            }
            return false;
        }

        public async Task<StudentViewModel> UpdateAsync(StudentViewModel vm)
        {
            try
            {
                Student obj = StudentDal.GetById(vm.Id);
                obj.Name = vm.Name;
                obj.UserName = vm.UserName;
                obj.Password = vm.Password;
                obj.PictureFileName = vm.PictureFileName!=null?
                    vm.PictureFileName:obj.PictureFileName;
                obj.CVFileName = vm.CVFileName != null ?
                    vm.CVFileName:obj.CVFileName;
                obj.Contact = vm.Contact;
                await StudentDal.UpdateAsync(obj);
            }
            catch (Exception ex )
            {

                throw;
            }
            return vm;
        }
    }
}
