using Microsoft.AspNetCore.Mvc;
using QuizCreation.Business.Services.Exams;
using QuizCreation.Business.Services.QuestionAnswers;
using QuizCreation.Entities.ViewModel;

namespace QuizCreation.Web.Controllers
{
    public class QuesitonAnswersController : Controller
    {
        private readonly IExamService _examService;
        private readonly IQuestionAnswerService _quesitonAnswerService;

        public QuesitonAnswersController(IExamService examService, IQuestionAnswerService quesitonAnswerService)
        {
            _examService = examService;
            _quesitonAnswerService = quesitonAnswerService;
        }

        public IActionResult Index(int pageNmuber=1,int pageSize=10)
        {
            return View(_quesitonAnswerService.GetAll(pageNmuber,pageSize));
        }
        public IActionResult Create()
        {
            var model = new QuestionAnswerViewModel();
            model.ExamList = _examService.GetAllExams();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(QuestionAnswerViewModel qnViewModel)
        {
            if (ModelState.IsValid)
            {
                await _quesitonAnswerService.AddAsync(qnViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(qnViewModel);   
        }
    }
}
