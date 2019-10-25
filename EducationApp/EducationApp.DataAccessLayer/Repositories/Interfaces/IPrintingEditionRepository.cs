using EducationApp.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.Interfaces
{
    public interface IPrintingEditionRepository : IGenericRepository<PrintingEdition>
    {
        Task<List<PrintingEdition>> GetAllIsDeleted();
        Task<List<PrintingEdition>> GetAll();
        IQueryable<PrintingEdition> Pagination();
    }
}
