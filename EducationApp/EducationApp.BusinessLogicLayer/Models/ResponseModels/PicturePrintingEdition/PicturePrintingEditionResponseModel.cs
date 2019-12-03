using EducationApp.BusinessLogicLayer.Models.ResponseModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.PicturePrintingEdition
{
    public class PicturePrintingEditionResponseModel : BaseResponseModel
    {
        public List<PicturePrintingEditionModel> PicturePrintingEditionsModels { get; set; }

        public PicturePrintingEditionResponseModel()
        {
            PicturePrintingEditionsModels = new List<PicturePrintingEditionModel>();
        }
    }
}
