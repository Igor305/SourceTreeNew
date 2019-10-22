using EducationApp.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EducationApp.DataAccessLayer.Repositories.Interfaces
{
    public interface IPrintingEditionRepository : IGenericRepository<PrintingEdition>
    {
        List<PrintingEdition> GetAllIsDeleted();
        List<PrintingEdition> GetAll();
        IQueryable<PrintingEdition> Pagination();
    }
}
