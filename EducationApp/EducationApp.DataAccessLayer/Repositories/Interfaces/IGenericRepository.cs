using System;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task<T> GetById(Guid id);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
