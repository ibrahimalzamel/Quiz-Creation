using Microsoft.Extensions.Logging;
using QuizCreation.DataAccess.Abstract;
using QuizCreation.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizCreation.Entities.Concrete;

namespace QuizCreation.Business.Services.Groups
{
    public class GroupManager : IGroupService
    {
        IGroupDal _groupDal;
        ILogger<GroupManager> _ilogger;
  
        public GroupManager(IGroupDal groupDal, ILogger<GroupManager> ilogger)
        {
            _groupDal = groupDal;
            _ilogger = ilogger;
        }

        public async Task<GroupViewModel> AddGroupAsync(GroupViewModel groupVM)
        {
            try
            {
                Group objGroup = groupVM.ConvertGroupViewModel(groupVM);
                await _groupDal.AddAsync(objGroup);
            }
            catch (Exception ex)
            {

                return null;
            }
            return groupVM;
        }

        public PagedResult<GroupViewModel> GetAllGroups(int pageIndex, int pageSize)
        {
            var model = new GroupViewModel();
            try
            {
                int ExcludeRecords = (pageSize * pageIndex) - pageSize;
                List<GroupViewModel> detailList = new List<GroupViewModel>();
                var modelList= _groupDal.GetAll().Skip(ExcludeRecords)
                    .Take(pageSize).ToList();
                var totalCount = _groupDal.GetAll().ToList();
                detailList = GroupListInfo(modelList);
                if (detailList !=null)
                {
                    model.GroupList = detailList;
                    model.TotalCount = totalCount.Count;
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
            }
            var result = new PagedResult<GroupViewModel>
            {
                Data = model.GroupList,
                TotalItems=model.TotalCount,
                PageNumber=pageIndex,
                PageSize=pageSize,
            };
            return result;
        }

        private List<GroupViewModel> GroupListInfo(List<Group> modelList)
        {
            return modelList.Select(o=>new GroupViewModel(o)).ToList();
        }

        public IEnumerable<Group> GetAllGroups()
        {
            try
            {
                var group = _groupDal.GetAll();
                return group;

            }
            catch (Exception ex)
            {

                _ilogger.LogError(ex.Message);
            }
            return Enumerable.Empty<Group>();
        }

        public GroupViewModel GetById(int groupId)
        {
            try
            {
                var group = _groupDal.GetById(groupId);
                return new  GroupViewModel(group);
                    
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
            }
            return null;
        }
    }
}
