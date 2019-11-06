using EducationApp.DataAccessLayer.Entities.Base;
using EducationApp.DataAccessLayer.Entities.Enum;
using System;

namespace EducationApp.DataAccessLayer.Entities
{
    public class OrderItem : BaseEntity
    {
        public int Amount { get; set; }
        public Currency Currency { get; set; }
        public decimal Count { get; set; }
        public decimal UnitPrice { get; set; }
        public Guid PrintingEditionId { get; set; }
        public PrintingEdition PrintingEdition { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
