using EducationApp.DataAccessLayer.AppContext;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Entities.Enum;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<List<PrintingEdition>> GetAll()
        {
            List<PrintingEdition> printingEditions = await _applicationContext.PrintingEditions.IgnoreQueryFilters().ToListAsync();
            return printingEditions;
        }

        public async Task<List<PrintingEdition>> GetAllWithoutRemove()
        {
             List <PrintingEdition> printingEditions = await _applicationContext.PrintingEditions.ToListAsync();
            return printingEditions;
        }

        public async Task<List<PrintingEdition>> Pagination(int Skip, int Take)
        {
            List<PrintingEdition> authors = await _applicationContext.PrintingEditions.ToListAsync();
            List<PrintingEdition> paginationAuthors = authors.Skip(Skip).Take(Take).ToList();
            return paginationAuthors;
        }

        public async Task<bool> CheckById(Guid id)
        {
            bool printingEdition = await _applicationContext.PrintingEditions.AnyAsync(x => x.Id == id);
            return printingEdition;
        }

        public List<PrintingEdition> SortById()
        {
            List<PrintingEdition> printingEditions = _applicationContext.PrintingEditions.OrderBy(x => x.Id).ToList();
            return printingEditions;
        }

        public List<PrintingEdition> SortByName()
        {
            List<PrintingEdition> printingEditions = _applicationContext.PrintingEditions.OrderBy(x => x.Name).ToList();
            return printingEditions;
        }

        public List<PrintingEdition> SortByPrice()
        {
            List<PrintingEdition> printingEditions = _applicationContext.PrintingEditions.OrderBy(x => x.Price).ToList();
            return printingEditions;
        }

        public async Task<List<PrintingEdition>> Filtration(string Name, decimal Price, Status Status)
        {
            List<PrintingEdition> printingEditions = await _applicationContext.PrintingEditions.Where(x => x.Name == Name).Where(x => x.Price == Price).Where(x => x.Status == Status).ToListAsync();
            return printingEditions;
        }

    }
}
