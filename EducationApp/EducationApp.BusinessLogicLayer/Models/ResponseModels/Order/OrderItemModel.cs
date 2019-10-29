using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Entities.Enum;
using System;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.Order
{
    public class OrderItemModel
    {
        public int Amount { get; set; }
        public Currency Currency { get; set; }
        public decimal Count { get; set; }
        public decimal UnitPrice { get; set; }
        public Guid PrintingEditionId { get; set; }
        public Guid OrderId { get; set; }
    }
}
