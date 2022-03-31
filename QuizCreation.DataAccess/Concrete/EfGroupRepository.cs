using QuizCreation.Core.DataAccess.Repositories;
using QuizCreation.DataAccess.Abstract;
using QuizCreation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.DataAccess.Concrete
{
    public class EfGroupRepository : GenericRepository<Group, QuizCreationDBContext>, IGroupDal
    {
        public EfGroupRepository(QuizCreationDBContext context) : base(context)
        {
        }
    }
}
