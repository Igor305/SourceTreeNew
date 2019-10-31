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
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {

        public AuthorRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
        public async Task<List<Author>> GetAll()
        {
            List<Author> all = await _applicationContext.Authors.IgnoreQueryFilters().ToListAsync();
            return all;
        }
        public async Task<List<Author>> GetAllWithoutRemove()
        {
            List<Author> all = await _applicationContext.Authors.ToListAsync();
            return all;
        }
        public async Task<bool> CheckById(Guid id)
        {
            bool author = await _applicationContext.Authors.AnyAsync(x => x.Id == id);
            return author;
        }
        public async Task<bool> CheckByName(string FirstName, string LastName)
        {
            bool author = false;
            bool authorFirstName = await _applicationContext.Authors.AnyAsync(x => x.FirstName == FirstName);
            bool authorLastName = await _applicationContext.Authors.AnyAsync(x => x.LastName == LastName);
            if (authorFirstName && authorLastName)
            {
                author = true;
            }
            return author;
        }
        public async Task<Author> GetByFullName(string FirstName, string LastName)
        {
            IQueryable<Author> listgetname = _applicationContext.Authors.Where(x => x.LastName == LastName);
            Author getname = await listgetname.FirstOrDefaultAsync(x => x.FirstName == FirstName);
            return getname;
        }
        public async Task<List<Author>> Pagination(int Skip, int Take)
        {
            List<Author> authors = await _applicationContext.Authors.ToListAsync();
            int count = authors.Count();
            List<Author> paginationAuthors = authors.Skip(Skip).Take(Take).ToList();
            return paginationAuthors;
        }
        public List<Author> Filter(string FirstName, string LastName, DateTime? DateBirthFrom, DateTime? DateBirthTo, DateTime? DateDeathFrom, DateTime? DateDeathTo)
        {
            List<Author> filtrauthor = _applicationContext.Authors.Where(x => x.FirstName == FirstName || string.IsNullOrEmpty(FirstName)).Where(x => x.LastName == LastName || string.IsNullOrEmpty(LastName))
                .Where(x => x.DateBirth >= DateBirthFrom || DateBirthFrom == null).Where(x => x.DateBirth <= DateBirthTo || DateBirthTo == null)
                .Where(x => x.DateBirth >= DateDeathFrom || DateDeathFrom == null).Where(x => x.DateBirth <= DateDeathTo || DateDeathTo == null).ToList();
            return filtrauthor;
        }
    }
}
