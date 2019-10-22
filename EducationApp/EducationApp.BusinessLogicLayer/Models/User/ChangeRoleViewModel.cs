using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.User
{
    public class ChangeRoleViewModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public IList<string> UserRoles { get; set; }
        public ChangeRoleViewModel()
        {
            UserRoles = new List<string>();
        }
    }
}
