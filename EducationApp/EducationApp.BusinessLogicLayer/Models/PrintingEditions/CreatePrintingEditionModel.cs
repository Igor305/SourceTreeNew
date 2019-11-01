using EducationApp.BusinessLogicLayer.Models.ResponseModels;
using EducationApp.DataAccessLayer.Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace EducationApp.BusinessLogicLayer.Models.PrintingEditions
{
    public class CreatePrintingEditionModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public Status Status { get; set; }
        public Currency Currency { get; set; }
    }
}
