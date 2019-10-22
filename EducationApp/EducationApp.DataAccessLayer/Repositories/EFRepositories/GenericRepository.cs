using EducationApp.DataAccessLayer.AppContext;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace EducationApp.DataAccessLayer.Repositories.EFRepositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected ApplicationContext _applicationContext;
        protected DbSet<T> _dbSet;

        public GenericRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
            _dbSet = applicationContext.Set<T>();
        }
        public T GetById(Guid id)
        {
            return _dbSet.Find(id);
        }
        public void Create(T entity)
        {
            _dbSet.Add(entity);
            _applicationContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _applicationContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _applicationContext.SaveChanges();
        }
    }
}
