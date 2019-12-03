using EducationApp.DataAccessLayer.Entities.Base;
using System;

namespace EducationApp.DataAccessLayer.Entities
{
    public class PicturePrintingEdition : BaseEntity
    {
        public byte[] Picture { get;set; }
        public Guid PrintingEditionId { get; set; }
        public PrintingEdition PrintingEdition { get; set; }
    }
}
