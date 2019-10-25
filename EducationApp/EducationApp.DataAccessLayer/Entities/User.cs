using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EducationApp.DataAccessLayer.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public Order Order { get; set; }
        public List<UserInRole> UserInRoly { get; set; }
    }
}
