using QuizCreation.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Entities.Concrete
{
    public class Exam : IEntity
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public int Time { get; set; }
        public int GroupId { get; set; }

        public Group Group { get; set; }
        public ICollection<ExamResult> ExamResults { get; set; }
        public ICollection<QuestionAnswer> QuestionAnswers { get; set; }

        public Exam()
        {
            ExamResults = new HashSet<ExamResult>();
            QuestionAnswers = new HashSet<QuestionAnswer>();
        }

        public Exam(int id, string title, string description, DateTime startDate, int time, int groupId)
        {
            Id = id;
            Title = title;
            Description = description;
            StartDate = startDate;
            Time = time;
            GroupId = groupId;
        }
    }
}
