using System;

namespace EducationApp.BusinessLogicLayer.Models.User
{
    public class ChangePasswordModel
    {
        public Guid Id { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
