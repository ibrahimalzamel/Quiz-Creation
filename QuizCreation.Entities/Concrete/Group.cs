using QuizCreation.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Entities.Concrete
{
    public class Group :IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Exam> Exams { get; set; }

        public Group()
        {
            Students = new HashSet<Student>();
            Exams = new HashSet<Exam>();
        }

        public Group(int id, string name, string description, int userId)
        {
            Id = id;
            Name = name;
            Description = description;
            UserId = userId;
        }
    }
}
