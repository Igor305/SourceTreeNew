﻿using EducationApp.BusinessLogicLayer.Models.Base;
using System;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.Orders
{
    public class UpdateOrderModel : BaseModel
    {
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public Guid PaymentId { get; set; }
        public List<UpdateOrderItemModel> updateOrderItemModels { get; set; }
    }
}
