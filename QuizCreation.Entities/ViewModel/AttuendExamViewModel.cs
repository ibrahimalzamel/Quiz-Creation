using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Entities.ViewModel
{
    public class AttuendExamViewModel
    {
        public int StudentId { get; set; }
        public string ExamName { get; set; }
        public List<QuestionAnswerViewModel> questionAnswerViewModels { get; set; }
        public string Message { get; set; }


    }
}
