using System;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class UpdateOrderModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
    }
}
