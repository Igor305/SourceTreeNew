using EducationApp.BusinessLogicLayer.Models.ResponseModels.Base;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.PrintingEditions
{
    public class PrintingEditionResponseModel : BaseResponseModel
    {
        public List<PrintingEditionModel> PrintingEditionModels { get; set; }

        public PrintingEditionResponseModel()
        {
            PrintingEditionModels = new List<PrintingEditionModel>();
        }
    }  
}
