using QuizCreation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Entities.ViewModel
{
    public class GroupViewModel
    {
        public GroupViewModel()
        {

        }
        public int Id { get; set; }
        [Required]
        [Display(Name = "Group Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        public int UserId { get; set; }

        public List<GroupViewModel> GroupList { get; set; }
        public List<StudentCheckBoxListViewModel> StudentCheckList { get; set; }
    public int TotalCount { get; set; }
        public GroupViewModel(Group model)
        {
            Id = model.Id;
            Name = model.Name ?? "";
            Description = model.Description ?? "";
            UserId = model.UserId;

        }
        public Group ConvertGroupViewModel(GroupViewModel vm)
        {
            return new Group
            {
                Id = vm.Id,
                Name = vm.Name??"",
                Description = vm.Description??"",
                UserId = vm.UserId,
            };
        }
    }
}
