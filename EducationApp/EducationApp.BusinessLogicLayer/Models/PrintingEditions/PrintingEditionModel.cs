using EducationApp.DataAccessLayer.Entities.Enum;

namespace EducationApp.BusinessLogicLayer.Models.PrintingEditions
{
    public class PrintingEditionModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Type { get; set; }
        public Status Status { get; set; }
        public Currency Currency { get; set; }
    }
}
