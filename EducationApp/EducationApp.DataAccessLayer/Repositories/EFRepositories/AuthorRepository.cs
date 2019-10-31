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
        public List<Author> Filter(string FirstName, string LastName, DateTime? DateBirthFirst, DateTime? DateBirthLast, DateTime? DateDeathFirst, DateTime? DateDeathLast)
        {
            List<Author> filtrauthor = _applicationContext.Authors.Where(x=>x.FirstName == FirstName).Where(x=>x.LastName == LastName).Where(x => x.DateBirth >= DateBirthFirst).Where(x => x.DateBirth <= DateBirthLast).Where(x => x.DateBirth >= DateDeathFirst).Where(x => x.DateBirth <= DateDeathLast).ToList();
            return filtrauthor;
        }
    }
}
