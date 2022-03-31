using QuizCreation.Entities.Concrete;
using QuizCreation.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Business.Services.Groups
{
    public interface IGroupService
    {
        PagedResult<GroupViewModel>GetAllGroups(int pageIndex, int pageSize);
        Task<GroupViewModel>AddGroupAsync(GroupViewModel groupVM);
        IEnumerable<Group> GetAllGroups();
        GroupViewModel GetById(int groupId);
    }
}
