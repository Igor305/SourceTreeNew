using EducationApp.BusinessLogicLayer.Models.ResponseModels.Base;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.User
{
    public class UserResponseModel : BaseResponseModel
    {
        public List<UserModel> UserModels { get; set; }

        public UserResponseModel()
        {
            UserModels = new List<UserModel>();
        }
    }
}
