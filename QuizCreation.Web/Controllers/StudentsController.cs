using Microsoft.AspNetCore.Mvc;
using QuizCreation.Business.Services.Exams;
using QuizCreation.Business.Services.QuestionAnswers;
using QuizCreation.Business.Services.Students;
using QuizCreation.Business.ViewModel;
using QuizCreation.Entities.ViewModel;

namespace QuizCreation.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IExamService _examService;
        private readonly IQuestionAnswerService _quesitonAnswerService;

        public StudentsController(IStudentService studentService, 
               IExamService examService, 
               IQuestionAnswerService quesitonAnswerService)
        {
            _studentService = studentService;
            _examService = examService;
            _quesitonAnswerService = quesitonAnswerService;
        }

        public IActionResult Index(int pageNumber=1, int pageSize=10)
        {
            return View(_studentService.GetAll(pageNumber,pageSize));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(StudentViewModel studentViewModel)
        {
            if (ModelState.IsValid)
            {
                await _studentService.AddAsync(studentViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(studentViewModel);
        }
        public IActionResult AttendExam()
        {
            var model = new AttuendExamViewModel();
            LoginViewModel sessionObj = HttpContext
                .Session.Get<LoginViewModel>("loginvm");
            if (sessionObj == null)
            {
                model.StudentId = Convert.ToInt32(sessionObj.Id);
                model.questionAnswerViewModels = new List<QuestionAnswerViewModel>();
                var todayExam =_examService.GetAllExams()
                    .Where(x=>x.StartDate.Date==DateTime.Today.Date).FirstOrDefault();
                if (todayExam==null)
                {
                    model.Message = "No Exam Scheduled todey";
                }
                else
                {
                    if (!_quesitonAnswerService.IsExamAttended(todayExam.Id, model.StudentId))
                    {
                        model.questionAnswerViewModels = _quesitonAnswerService.GetAllQuestionAnswersByExamId(todayExam.Id).ToList();
                        model.ExamName = todayExam.Title;
                        model.Message = "";

                    }
                    else
                        model.Message = "You have already attend this exam";
                }
                return View(model);
            }
            return RedirectToAction("Login","Account");
        }   
        [HttpPost]
        public IActionResult AttendExam(AttuendExamViewModel attuendExamViewModel)
        {
            bool result = _studentService.SetExamResult(attuendExamViewModel);
            return RedirectToAction("AttendExam");
        }
        public IActionResult Result(string studentId)
        {
            var model = _studentService.GetAllExamResult(Convert.ToInt32(studentId));
            return View(model);
        }
        public IActionResult ViewResult()
        {
            LoginViewModel sessionObj = HttpContext.Session.Get<LoginViewModel>("loginvm");
            if (sessionObj!=null)
            {
                var model = _studentService.GetAllExamResult(Convert.ToInt32(sessionObj.Id));
                return View(model);
            }
            return RedirectToAction("Login","Account");
        }
        public IActionResult Profile()
        {
            LoginViewModel sessionObj = HttpContext.Session.Get<LoginViewModel>("loginvm");
            if (sessionObj!=null)
            {
                var model = _studentService.GetStudentDetails(Convert.ToInt32(sessionObj.Id));
                if (model.PictureFileName!=null)
                {
                    model.PictureFileName = ConfigurationManager.GetFilePath() + model.PictureFileName;
                    
                }
                model.CVFileName = ConfigurationManager.GetFilePath() + model.CVFileName;
                return View(model);
            }
            return RedirectToAction("Login", "Account");
        }
        public IActionResult Profile([FromForm]StudentViewModel studentViewModel)
        {
            if (studentViewModel.PictureFile!=null)
            {
                studentViewModel.PictureFileName = SaveStudentFile(studentViewModel.PictureFile);
                if (studentViewModel.CVFile!=null)
                    studentViewModel.CVFileName = SaveStudentFile(studentViewModel.CVFile);
                _studentService.UpdateAsync(studentViewModel);
                return RedirectToAction("Profile");
            }
            return View(studentViewModel);
        }

        private string SaveStudentFile(IFormFile pictureFile)
        {
            if (pictureFile ==null)
            {
                return string.Empty;
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/file");
            return SaveFile(path, pictureFile);
        }

        private string SaveFile(string path, IFormFile pictureFile)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var filename = Guid.NewGuid().ToString()+"."+pictureFile.FileName.Split('.')
                [pictureFile.FileName.Split('.').Length-1];
            path = Path.Combine(path, filename);
            using (Stream stream = new FileStream(path,FileMode.Create))
            {
                pictureFile.CopyTo(stream);
            }
            return filename; 
        }
    }
}
