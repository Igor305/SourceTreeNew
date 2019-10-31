using EducationApp.DataAccessLayer.AppContext;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

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

        public async Task<T> GetById(Guid id)
        {
            T find = await _dbSet.FindAsync(id);
            return find;              
        }

        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
