using EducationApp.BusinessLogicLayer.Models.ResponseModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.ImagePrintingEdition
{
    public class ImagePrintingEditionResponseModel : BaseResponseModel
    {
        public List<ImagePrintingEditionModel> ImagePrintingEditionsModels { get; set; }

        public ImagePrintingEditionResponseModel()
        {
            ImagePrintingEditionsModels = new List<ImagePrintingEditionModel>();
        }
    }
}
