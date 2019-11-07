using EducationApp.DataAccessLayer.Entities.Base;
using System;
using System.Collections.Generic;

namespace EducationApp.DataAccessLayer.Entities
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }
        public Guid PaymentId { get; set; }
        public Payment Payment { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
