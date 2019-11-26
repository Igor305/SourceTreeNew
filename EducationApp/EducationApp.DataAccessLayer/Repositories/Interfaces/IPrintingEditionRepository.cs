using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.Interfaces
{
    public interface IPrintingEditionRepository : IGenericRepository<PrintingEdition>
    {
        Task<List<PrintingEdition>> GetAll();
        Task<List<PrintingEdition>> GetAllWithoutRemove();
        Task<bool> CheckById(Guid id);
        Task<List<PrintingEdition>> Pagination(int Skip, int Take);
        List<PrintingEdition> SortById();
        List<PrintingEdition> SortByName();
        List<PrintingEdition> SortByPrice();
        Task<List<PrintingEdition>> Filtration(string Name, decimal Price, Status Status);
    }
}
