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
            List<User> users = await _applicationContext.Users.IgnoreQueryFilters().ToListAsync();
            return users;
        }

        public async Task<List<User>> GetAllWithoutRemove()
        {
            List<User> users = await _applicationContext.Users.ToListAsync();
            return users;
        }

        public async Task<User> GetByIdByAll(Guid id)
        {
            User user = await _applicationContext.Users.IgnoreQueryFilters().FirstAsync(x => x.Id == id);
            return user;
        }
        public async Task<bool> CheckById(Guid id)
        {
            bool user = await _applicationContext.Users.AnyAsync(x => x.Id == id);
            return user;
        }
        public async Task<bool> CheckByIdByAll(Guid id)
        {
            bool user = await _applicationContext.Users.IgnoreQueryFilters().AnyAsync(x => x.Id == id);
            return user;
        }
    }
}
