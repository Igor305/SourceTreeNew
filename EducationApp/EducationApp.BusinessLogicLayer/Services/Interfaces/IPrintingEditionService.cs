using EducationApp.BusinessLogicLayer.Models.PrintingEditions;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.PrintingEditions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        Task<PrintingEditionResponseModel> GetAllIsDeleted();
        Task<PrintingEditionResponseModel> GetAll();
        PrintingEditionResponseModel Pagination(PaginationPagePrintingEditionModel paginationPagePrintingEditionModel);
        Task<PrintingEditionResponseModel> Buy(BuyPrintingEditionModel buyPrintingEditionModel);
        Task<PrintingEditionResponseModel> Create(CreatePrintingEditionModel createPrintingEditionModel);
        Task<PrintingEditionResponseModel> Update(UpdatePrintingEditionModel updatePrintingEditionModel);
        Task<PrintingEditionResponseModel> Delete(DeletePrintingEditionModel deletePrintingEditionModel);
        Task<PrintingEditionResponseModel> Sort(SortPrintingEditionModel sortPrintingEditionModel);
        Task<PrintingEditionResponseModel> Filter(FiltrationPrintingEditionModel filtrationPrintingEditionModel);

    }
}
