using EducationApp.DataAccessLayer.AppContext;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EducationApp.DataAccessLayer.Repositories.EFRepositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
        public List<User> GetAllIsDeleted()
        {
            var allIsDeleted = _applicationContext.Users.IgnoreQueryFilters().ToList();
            return allIsDeleted;
        }
        public List<User> GetAll()
        {
            var all = _applicationContext.Users.ToList();
            return all;
        }
    }
}
