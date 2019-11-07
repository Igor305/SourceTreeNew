using EducationApp.DataAccessLayer.Entities.Base;
using EducationApp.DataAccessLayer.Entities.Enum;
using System;

namespace EducationApp.DataAccessLayer.Entities
{
    public class OrderItem : BaseEntity
    {
        public long Amount { get; set; }
        public Currency Currency { get; set; }
        public int Count { get; set; }
        public long UnitPrice { get; set; }
        public Guid PrintingEditionId { get; set; }
        public PrintingEdition PrintingEdition { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}