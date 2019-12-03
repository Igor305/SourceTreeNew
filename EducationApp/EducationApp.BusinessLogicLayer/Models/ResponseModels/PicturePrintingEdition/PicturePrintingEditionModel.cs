using System;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.PicturePrintingEdition
{
    public class PicturePrintingEditionModel
    {
        public Guid Id { get; set; }
        public string Picture { get; set; }
        public Guid PrintingEditionId { get; set; }
    }
}
