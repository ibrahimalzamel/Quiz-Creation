using Microsoft.AspNetCore.Mvc;
using QuizCreation.Business.Services.Groups;
using QuizCreation.Business.Services.Students;
using QuizCreation.Entities.ViewModel;

namespace QuizCreation.Web.Controllers
{
    public class GroupsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IGroupService _groupService;

        public GroupsController(IStudentService studentService, IGroupService groupService)
        {
            _studentService = studentService;
            _groupService = groupService;
        }

        public IActionResult Index(int pageNumber=1 , int pageSize=10)
        {
            return View(_groupService.GetAllGroups(pageNumber,pageSize));
        }
        public IActionResult Create()
        {
            return View();
        }
        
        public async Task<IActionResult> Create(GroupViewModel groupViewModel)
        {
            if (ModelState.IsValid)
            {
                groupViewModel.UserId = 1;
                await _groupService.AddGroupAsync(groupViewModel);
                return RedirectToAction(nameof(Index));                   
            }
            return View(groupViewModel);
        }
        
        public IActionResult Details(string groupId)
        {
            var model = _groupService.GetById(Convert.ToInt32(groupId));
            model.StudentCheckList = _studentService.GetAllStudents().Select(
                a => new StudentCheckBoxListViewModel()
                {
                    Id = a.Id,
                    Name = a.Name,
                    Selected=a.GroupId==Convert.ToInt32(groupId)
                }).ToList();   
            return View(model); 
        }
        [HttpPost]
        public IActionResult Details(GroupViewModel groupViewModel)
        {
            bool result = _studentService.SetGroupIdToStudents(groupViewModel);
            if (result)
                return RedirectToAction("Details",new {groupId=groupViewModel.Id});
            return View(groupViewModel);
        }
    }

}
