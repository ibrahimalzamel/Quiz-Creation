using QuizCreation.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Core.DataAccess.Repositories
{
    public interface  IGenericRepository<T> where T : class
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T GetById(object id);
        void Add(T entity);
        void Delete(T entityToDelete);
        void Update(T entityToUpdate);
        void DeleteById(object id);
        //Async
        Task<T> GetByIdAsync(object id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entityToUpdate);
        Task<T> DeleteAsync(T entityToDelete);


    }
}
