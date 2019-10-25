using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.DataAccessLayer.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public List<UserInRole> UserInRoly { get; set; }
    }
}
