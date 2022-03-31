using QuizCreation.Business.ViewModel;
using QuizCreation.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Business.Services.Account
{
    public interface IAccountService
    {
        LoginViewModel Login(LoginViewModel vm);
        bool AddTeacher(UserViewModel vm);
        PagedResult<UserViewModel> GetAllTeachers(int pageNumber, int pageSize);
    }
}
