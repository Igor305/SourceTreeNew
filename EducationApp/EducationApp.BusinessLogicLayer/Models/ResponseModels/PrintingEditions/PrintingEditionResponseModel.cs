using EducationApp.BusinessLogicLayer.Models.ResponseModels.Base;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.PrintingEditions
{
    public class PrintingEditionResponseModel : BaseResponseModel
    {
        public List<PrintingEditionModel> PrintingEditionModel { get; set; }

        public PrintingEditionResponseModel()
        {
            PrintingEditionModel = new List<PrintingEditionModel>();
        }
    }  
}
