using EducationApp.DataAccessLayer.Entities.Base;
using System;
using System.Collections.Generic;

namespace EducationApp.DataAccessLayer.Entities
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Description { get; set; }
        public Guid PaymentId { get; set; }
        public Payment Payment { get; set; }
        public List<OrderItem> OrderItem { get; set; }
    }
}
