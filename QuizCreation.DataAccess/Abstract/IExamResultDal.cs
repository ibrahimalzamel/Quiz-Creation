using QuizCreation.Core.DataAccess.Repositories;
using QuizCreation.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.DataAccess.Abstract
{
    public interface IExamResultDal : IGenericRepository<ExamResult>
    {
    }
}
