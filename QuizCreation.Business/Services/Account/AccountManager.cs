using Microsoft.Extensions.Logging;
using QuizCreation.Business.Services.Students;
using QuizCreation.Business.ViewModel;
using QuizCreation.DataAccess.Abstract;
using QuizCreation.Entities.Concrete;
using QuizCreation.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Business.Services.Account
{
    public class AccountManager : IAccountService
    {
        IUserDal _userDal;
        IStudentDal _studentDal;
        ILogger<StudentManager> _ilogger;
        public AccountManager(IUserDal userDal, IStudentDal studentDal, ILogger<StudentManager> ilogger)
        {
            _userDal = userDal;
            _studentDal = studentDal;
            _ilogger = ilogger;
        }
        public bool AddTeacher(UserViewModel vm)
        {
            try
            {
                User obj = new User()
                {
                    Name = vm.Name,
                    UserName = vm.UserName,
                    Password = vm.Password,
                    Role = (int)EnumRoles.Teacher
                };
                _userDal.AddAsync(obj);
            }
            catch (Exception ex)
            {

                _ilogger.LogError(ex.Message);
                return false;
            }
            return true;
        }

        public PagedResult<UserViewModel> GetAllTeachers(int pageNumber, int pageSize)
        {
            var model = new UserViewModel();
            try
            {
                int ExcludeRecorde = (pageSize * pageNumber) - pageSize;
                List<UserViewModel> detailList = new List<UserViewModel>();
                var modelList = _userDal.GetAll()
                    .Where(x => x.Role == (int)EnumRoles.Teacher).Skip(ExcludeRecorde)
                    .Take(pageSize).ToList();
                detailList = ListInfo(modelList);
                if (detailList != null)
                {
                    model.UserList = detailList;
                    model.TotalCount = _userDal.GetAll()
                        .Count(x=>x.Role == (int)EnumRoles.Teacher);    
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
            }
            var result = new PagedResult<UserViewModel>
            {
                Data = model.UserList,
                TotalItems = model.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        private List<UserViewModel> ListInfo(List<User> modelList)
        {
            return modelList.Select(o => new UserViewModel(o)).ToList();
        }

        public LoginViewModel Login(LoginViewModel vm)
        {
            if (vm.Role==(int)EnumRoles.Admin|| vm.Role == (int)EnumRoles.Teacher)
            {
                var user = _userDal.GetAll().FirstOrDefault(
                    a=>a.UserName==vm.UserName.Trim()
                  &&a.Password==vm.Password.Trim()
                  &&a.Role==vm.Role
                  );
                if (user!=null)
                {
                    vm.Id = user.Id;
                    return vm;
                }
            }
            else
            {
                var student = _studentDal.GetAll()
                    .FirstOrDefault(
                    a => a.UserName == vm.UserName.Trim()
                 && a.Password == vm.Password.Trim()
                 );
                if (student!=null)
                {
                     vm.Id = student.Id ;
                }
                return vm;
            }
            return null;
        }
    }
}
