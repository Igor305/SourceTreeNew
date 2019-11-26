using EducationApp.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<List<User>> GetAll();
        Task<List<User>> GetAllWithoutRemove();
        Task<User> GetByIdByAll(Guid id);
        Task<bool> CheckById(Guid id);
        Task<bool> CheckByIdByAll(Guid id);
    }
}
