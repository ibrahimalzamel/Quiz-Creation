using QuizCreation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Entities.ViewModel
{
    public class UserViewModel
    {
        public UserViewModel()
        {

        }
        public UserViewModel(User model)
        {
            Id = model.Id;
            Name = model.Name??"";
            UserName = model.UserName;
            Password = model.Password;
            Role = model.Role;
        }
        public User ConvertViewModel(UserViewModel vm)
        {
            return new User
            {
                Id = vm.Id,
                Name = vm.Name,
                UserName = vm.UserName,
                Password = vm.Password,
                Role = vm.Role,

            };
        }
        public int Id { get; set; }
        [Required]
        [Display(Name ="Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public int Role { get; set; }
        public List<UserViewModel> UserList { get; set; }
        public int TotalCount { get; set; }
    }
}
