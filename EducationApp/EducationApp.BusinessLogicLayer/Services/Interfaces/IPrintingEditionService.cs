using EducationApp.BusinessLogicLayer.Models.PrintingEditions;
using EducationApp.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IPrintingEditionService
    {
        List<PrintingEdition> GetAllIsDeleted();
        List<PrintingEdition> GetAll();
        List<PrintingEdition> Pagination(PaginationPagePrintingEditionModel paginationPagePrintingEditionModel);
        object Buy(BuyPrintingEditionModel buyPrintingEditionModel);
        void Create(CreatePrintingEditionModel createPrintingEditionModel);
        void Update(UpdatePrintingEditionModel updatePrintingEditionModel);
        void Delete(DeletePrintingEditionModel deletePrintingEditionModel);
        object Sort(SortPrintingEditionModel sortPrintingEditionModel);
        object Filter(FiltrationPrintingEditionModel filtrationPrintingEditionModel);

    }
}
