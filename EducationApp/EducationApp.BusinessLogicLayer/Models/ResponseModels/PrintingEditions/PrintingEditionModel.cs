using EducationApp.DataAccessLayer.Entities.Enum;
using System;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.PrintingEditions
{
    public class PrintingEditionModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public Status Status { get; set; }
        public Currency Currency { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
