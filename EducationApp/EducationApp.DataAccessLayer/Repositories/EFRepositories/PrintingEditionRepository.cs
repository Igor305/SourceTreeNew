using EducationApp.DataAccessLayer.AppContext;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.EFRepositories
{
    public class PrintingEditionRepository : GenericRepository<PrintingEdition>, IPrintingEditionRepository
    {
        public PrintingEditionRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }
        public async Task<List<PrintingEdition>> GetAllIsDeleted()
        {
            var allIsDeleted = await _applicationContext.PrintingEditions.IgnoreQueryFilters().ToListAsync();
            return allIsDeleted;
        }
        public async Task<List<PrintingEdition>> GetAll()
        {
            var all = await _applicationContext.PrintingEditions.ToListAsync();
            return all;
        }
        public IQueryable<PrintingEdition> Pagination()
        {
            IQueryable<PrintingEdition> source = _applicationContext.PrintingEditions.Include(x => x.AutorInPrintingEdition);
            return source;
        }
    }
}
