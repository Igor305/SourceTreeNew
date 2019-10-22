using EducationApp.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace EducationApp.DataAccessLayer.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        List<User> GetAllIsDeleted();
        List<User> GetAll();
    }
}
