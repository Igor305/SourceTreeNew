using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.Account
{
    public class RoleModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public RoleModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
