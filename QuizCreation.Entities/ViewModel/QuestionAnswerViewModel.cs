using QuizCreation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Entities.ViewModel
{
    public class QuestionAnswerViewModel
    {
        public QuestionAnswerViewModel()
        {

        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Exam")]
        public int ExamId { get; set; }
        [Required]
        [Display(Name = "Question")]
        public string Question { get; set; }
        [Required]
        [Display(Name = "Answer")]
        public int Answer { get; set; }
        [Required]
        [Display(Name = "Option 1")]
        public string Option1 { get; set; }
        [Required]
        [Display(Name = "Option 2")]
        public string Option2 { get; set; }
        [Required]
        [Display(Name = "Option 3")]
        public string Option3 { get; set; }
        [Required]
        [Display(Name = "Option 4")]
        public string Option4 { get; set; }

        public List<QuestionAnswerViewModel> QuestionAnswerList { get; set; }
        public IEnumerable<Exam> ExamList { get; set; }
        public int TotalCount { get; set; }
        public int SelectedAnswer { get; set; }
        public QuestionAnswerViewModel(QuestionAnswer model)
        {
            Id = model.Id;
            Question = model.Question??"";
            Answer = model.Answer;
            ExamId = model.ExamId;
            Option1 = model.Option1??"";
            Option2 = model.Option2??"";
            Option3 = model.Option3??"";
            Option4 = model.Option4??"";
        }

        public QuestionAnswer ConvertViewModel(QuestionAnswerViewModel vm)

        {
            return new QuestionAnswer
            {
                Id = vm.Id,
                Question = vm.Question?? "",
                Answer = vm.Answer,
                ExamId = vm.ExamId,
                Option1 = vm.Option1?? "",
                Option2 = vm.Option2?? "",
                Option3 = vm.Option3?? "",
                Option4 = vm.Option4?? "",
            };
        }
    }
}
