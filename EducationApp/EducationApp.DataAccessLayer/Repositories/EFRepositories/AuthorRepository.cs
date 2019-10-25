﻿using EducationApp.DataAccessLayer.AppContext;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Author>> GetAllIsDeleted()
        {
            List<Author> all = await _applicationContext.Authors.IgnoreQueryFilters().ToListAsync();
            return all;
        }
        public async Task<List<Author>> GetAll()
        {
            List<Author> all = await _applicationContext.Authors.ToListAsync();
            return all;
        }
        public async Task<Author> GetName(string FirstName, string LastName)
        {
            IQueryable<Author> listgetname = _applicationContext.Authors.Where(x => x.LastName == LastName);
            Author getname = await listgetname.FirstOrDefaultAsync(x => x.FirstName == FirstName);
            return getname;
        }
        public IQueryable<Author> Pagination()
        {
            IQueryable<Author> authors = _applicationContext.Authors.Include(x => x.AutorInPrintingEdition);
            return authors;
        }
    }
}
