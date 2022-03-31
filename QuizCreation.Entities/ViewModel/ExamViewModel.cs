using QuizCreation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Entities.ViewModel
{
    public class ExamViewModel
    {
        public ExamViewModel()
        {

        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Exam Name")]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int Time { get; set; }
        public int GroupId { get; set; }
        public int TotalCount { get; set; }
        public List<ExamViewModel> ExamList { get; set; }
        public IEnumerable<Group> GroupList { get; set; }
        public ExamViewModel(Exam model)
        {
            Id = model.Id;
            Title = model.Title??"";
            Description = model.Description??"";
            StartDate = model.StartDate;
            Time = model.Time;
            GroupId = model.GroupId;
        }
        public Exam ConvertExamViewModel(ExamViewModel vm)
        {
            return new Exam
            {
                Id = vm.Id,
                Title = vm.Title??"",
                Description = vm.Description??"",
                StartDate = vm.StartDate,
                Time = vm.Time,
                GroupId = vm.GroupId,
            };
        }
    }
}
