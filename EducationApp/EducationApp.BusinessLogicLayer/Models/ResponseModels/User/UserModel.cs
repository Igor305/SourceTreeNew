using System;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.User
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
