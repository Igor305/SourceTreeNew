using EducationApp.DataAccessLayer.Entities.Enum;
using System;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class CreateOrderModel
    {
        public string TypeOfPaymentCard { get; set; }
        public string Description { get; set; }
        public Currency Currency { get; set; }
        public Guid UserId { get; set; }
        public List<OrderPrintingEdition> OrderPrintingEditions { get; set; }
    }
}
