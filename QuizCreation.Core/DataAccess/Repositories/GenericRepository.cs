using Microsoft.EntityFrameworkCore;
using QuizCreation.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuizCreation.Core.DataAccess.Repositories
{
    public class GenericRepository<TEntity, TContext> :IGenericRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        public GenericRepository()
        {
        }
        protected TContext Context { get; }
        public GenericRepository(TContext context)
        {
            Context = context;
        }
        public void Add(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            Context.SaveChanges();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Added;
            await Context.SaveChangesAsync();
            return entity;
        }

        public void Delete(TEntity entityToDelete)
        {
            Context.Entry(entityToDelete).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public async Task<TEntity> DeleteAsync(TEntity entityToDelete)
        {
            Context.Entry(entityToDelete).State = EntityState.Deleted;
            await Context.SaveChangesAsync();
            return entityToDelete;
        }

        public void DeleteById(object id)
        {
            TEntity entityToDelet = Context.Set<TEntity>().Find(id);
            Context.Entry(entityToDelet).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {

            return filter == null
                      ? Context.Set<TEntity>().ToList()
                      : Context.Set<TEntity>().Where(filter).ToList();
        }

        public TEntity GetById(object id)
        {
                return Context.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
           
           return await Context.Set<TEntity>().FindAsync(id);
        }

        public void Update(TEntity entityToUpdate)
        {
            Context.Entry(entityToUpdate).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public async Task<TEntity> UpdateAsync(TEntity entityToUpdate)
        {
            Context.Entry(entityToUpdate).State = EntityState.Modified;
            await Context.SaveChangesAsync();
            return entityToUpdate;
        }
    }
}
