using EducationApp.DataAccessLayer.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.DataAccessLayer.Entities
{
    public class Payment : Basic
    {
        public string TransactionId { get; set; }
        public Order Order { get; set; }
    }
}
