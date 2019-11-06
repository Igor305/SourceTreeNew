using EducationApp.BusinessLogicLayer.Models.PrintingEditions;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.PrintingEditions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        Task<PrintingEditionResponseModel> GetAll();
        Task<PrintingEditionResponseModel> GetAllWithoutRemove();
        Task<PrintingEditionResponseModel> Pagination(PaginationPrintingEditionModel paginationPrintingEditionModel);
        Task<PrintingEditionResponseModel> GetById(Guid id);
        Task<PrintingEditionResponseModel> Buy(Guid id);
        Task<PrintingEditionResponseModel> Create(CreatePrintingEditionModel createPrintingEditionModel);
        Task<PrintingEditionResponseModel> Update(Guid id, CreatePrintingEditionModel createPrintingEditionModel);
        Task<PrintingEditionResponseModel> Delete(Guid id);
        Task<PrintingEditionResponseModel> Sort(SortPrintingEditionModel sortPrintingEditionModel);
        Task<PrintingEditionResponseModel> Filter(FiltrationPrintingEditionModel filtrationPrintingEditionModel);

    }
}
