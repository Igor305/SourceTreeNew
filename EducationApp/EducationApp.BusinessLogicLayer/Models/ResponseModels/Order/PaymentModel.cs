using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.Order
{
    public class PaymentModel
    {
        public Guid Id { get; set; }
        public string TransactionId { get; set; }
    }
}
