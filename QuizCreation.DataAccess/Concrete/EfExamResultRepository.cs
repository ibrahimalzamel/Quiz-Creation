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
    public class EfExamResultRepository : GenericRepository<ExamResult, QuizCreationDBContext>, IExamResultDal
    {
        public EfExamResultRepository(QuizCreationDBContext context) : base(context)
        {
        }
    }
}
