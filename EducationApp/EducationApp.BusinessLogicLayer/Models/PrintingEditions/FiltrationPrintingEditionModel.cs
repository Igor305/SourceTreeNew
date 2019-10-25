using EducationApp.DataAccessLayer.Entities.Enum;

namespace EducationApp.BusinessLogicLayer.Models.PrintingEditions
{
    public class FiltrationPrintingEditionModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Status Status { get; set; }
    }
}
