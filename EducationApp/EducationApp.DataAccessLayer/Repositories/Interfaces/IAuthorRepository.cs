using EducationApp.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EducationApp.DataAccessLayer.Repositories.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        List<Author> GetAll();
        List<Author> GetAllIsDeleted();
        IQueryable<Author> Pagination();
    }
}
