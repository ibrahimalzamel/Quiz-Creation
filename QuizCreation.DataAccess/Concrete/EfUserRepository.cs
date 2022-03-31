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
    public class EfUserRepository : GenericRepository<User, QuizCreationDBContext>, IUserDal
    {
        public EfUserRepository(QuizCreationDBContext context) : base(context)
        {
        }
    }
}
