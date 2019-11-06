using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.UserInRole
{
    public class UserInRoleModel
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
