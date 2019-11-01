using EducationApp.DataAccessLayer.AppContext;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.EFRepositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
        public async Task<List<User>> GetAll()
        {
            List<User> allIsDeleted = await _applicationContext.Users.IgnoreQueryFilters().ToListAsync();
            return allIsDeleted;
        }
        public async Task<List<User>> GetAllWithoutRemove()
        {
            List<User> all = await _applicationContext.Users.ToListAsync();
            return all;
        }
        public async Task<User> GetByIdAll(Guid Id)
        {
            User finduser = await _applicationContext.Users.IgnoreQueryFilters().FirstOrDefaultAsync(x=>x.Id == Id);
            return finduser;
        }
    }
}
