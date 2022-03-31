using Microsoft.AspNetCore.Mvc;
using QuizCreation.Business.Services.Account;
using QuizCreation.Business.ViewModel;
using QuizCreation.Entities.ViewModel;

namespace QuizCreation.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Login()
        {
            LoginViewModel loginViewModel = HttpContext.Session.Get<LoginViewModel>("loinvm");
            if (loginViewModel==null)
                 return View();
            else
            {
                return RedirectUser(loginViewModel);
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Set<LoginViewModel>("loginvm", null);
            return RedirectToAction("Login");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                LoginViewModel loginVM = _accountService.Login(loginViewModel);
                if (loginVM != null)
                {
                    HttpContext.Session.Set<LoginViewModel>("loginvm",loginVM);
                    return RedirectUser(loginVM);
                }
            }
                return View(loginViewModel);
           
        }
        public IActionResult RedirectUser(LoginViewModel loginViewModel)
        {
            if (loginViewModel.Role==(int)EnumRoles.Admin)
            {
                return RedirectToAction("Index", "User");
            }
            else if (loginViewModel.Role==(int)EnumRoles.Teacher)
            {
                return RedirectToAction("Index", "Exam");
            }
            return RedirectToAction("Profile", "Student");
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
