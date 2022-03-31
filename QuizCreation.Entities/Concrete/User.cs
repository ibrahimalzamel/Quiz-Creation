using QuizCreation.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }

        public ICollection<Group> Groups { get; set; }
        public User()
        {
            Groups = new HashSet<Group>(); 
        }

        public User(int id, string name, string userName, string password, int role)
        {
            Id = id;
            Name = name;
            UserName = userName;
            Password = password;
            Role = role;
        }
    }
}
