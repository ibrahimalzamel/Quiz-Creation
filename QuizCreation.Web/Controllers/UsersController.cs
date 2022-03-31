using Microsoft.AspNetCore.Mvc;
using QuizCreation.Business.Services.Account;
using QuizCreation.Entities.ViewModel;

namespace QuizCreation.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IAccountService _accountService;

        public UsersController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index(int pageNumer=1 , int pageSize=10)
        {
            return View(_accountService.GetAllTeachers(pageNumer,pageSize));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                _accountService.AddTeacher(userViewModel);
                return RedirectToAction("Index");
            }   
            return View(userViewModel);
        }
    }
}
