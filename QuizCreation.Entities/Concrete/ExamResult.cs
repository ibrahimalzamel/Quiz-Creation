using QuizCreation.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Entities.Concrete
{
    public class ExamResult: IEntity
    {
   

        public int Id { get; set; }
        public int StudentId { get; set; }
        public int? ExamId { get; set; }
        public int QuestionAnswerId { get; set; }
        public int Answer { get; set; }

        public Student Student { get; set; }
        public Exam Exam { get; set; }
        public QuestionAnswer QuestionAnswer { get; set; }
        public ExamResult()
        {

        }
        public ExamResult(int id, int studentId, int? examId, int questionAnswerId, int answer)
        {
            Id = id;
            StudentId = studentId;
            ExamId = examId;
            QuestionAnswerId = questionAnswerId;
            Answer = answer;
        }

    }
}
