using EducationApp.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.Interfaces
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<List<Author>> GetAllIsDeleted();
        Task<List<Author>> GetAll();
        Task<Author> GetName(string FirstName, string LastName);
        IQueryable<Author> Pagination();
    }
}
