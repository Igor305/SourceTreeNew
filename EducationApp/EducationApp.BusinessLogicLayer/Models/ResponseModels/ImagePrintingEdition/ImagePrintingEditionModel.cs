using System;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.ImagePrintingEdition
{
    public class ImagePrintingEditionModel
    {
        public Guid Id { get; set; }
        public string Image { get; set; }
        public Guid PrintingEditionId { get; set; }
    }
}
