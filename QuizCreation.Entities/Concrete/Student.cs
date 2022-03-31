using QuizCreation.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Entities.Concrete
{
    public class Student : IEntity
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Contact { get; set; }
        public string CVFileName { get; set; }
        public string PictureFileName { get; set; }
        public int? GroupId { get; set; }

        public Group Group { get; set; }
        public ICollection<ExamResult> ExamResults { get; set; }

        public Student()
        {
            ExamResults = new HashSet<ExamResult>();
        }

        public Student(int id, string name, string userName, 
                       string password, string contact, string cVFileName, 
                       string pictureFileName, int groupId)
        {
            Id = id;
            Name = name;
            UserName = userName;
            Password = password;
            Contact = contact;
            CVFileName = cVFileName;
            PictureFileName = pictureFileName;
            GroupId = groupId;
        }
    }
}
