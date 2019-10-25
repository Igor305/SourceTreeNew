using EducationApp.BusinessLogicLayer.Models.Base;

namespace EducationApp.BusinessLogicLayer.Models.User
{
    public class UpdateUserModel : BaseModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
