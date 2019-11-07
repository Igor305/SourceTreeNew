using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Entities.Enum;
using System;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.Order
{
    public class OrderItemModel
    {
        public long Amount { get; set; }
        public Currency Currency { get; set; }
        public int Count { get; set; }
        public long UnitPrice { get; set; }
        public Guid PrintingEditionId { get; set; }
        public Guid OrderId { get; set; }
    }
}
