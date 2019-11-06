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
        Task<bool> CheckById(Guid id);
    }
}
