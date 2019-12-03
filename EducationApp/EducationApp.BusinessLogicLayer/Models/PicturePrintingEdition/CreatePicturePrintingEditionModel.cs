using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.PicturePrintingEdition
{
    public class CreatePicturePrintingEditionModel
    {
        public string Picture { get; set; }
        public Guid PrintingEditionId { get; set; }
    }
}
