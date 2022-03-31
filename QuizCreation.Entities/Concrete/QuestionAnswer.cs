using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Entities.Concrete
{
    public class QuestionAnswer
    {
        public int Id { get; set; }
        public int ExamId { get; set; }
        public string Question { get; set; }
        public int Answer { get; set; }
        public string Option1 { get; set; }
        public string Option2 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }

        public Exam Exam { get; set; }
        public ICollection<ExamResult> ExamResults { get; set; }

        public QuestionAnswer()
        {
            ExamResults =new HashSet<ExamResult>();
        }

        public QuestionAnswer(int id, int examId, string question, int answer, string option1, string option2, string option3, string option4)
        {
            Id = id;
            ExamId = examId;
            Question = question;
            Answer = answer;
            Option1 = option1;
            Option2 = option2;
            Option3 = option3;
            Option4 = option4;
        }
    }
}
