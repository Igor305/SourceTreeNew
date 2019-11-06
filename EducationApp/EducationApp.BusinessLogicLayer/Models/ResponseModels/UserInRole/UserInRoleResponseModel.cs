using EducationApp.BusinessLogicLayer.Models.ResponseModels.Base;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.UserInRole
{
    public class UserInRoleResponseModel : BaseResponseModel
    {
        public List<UserInRoleModel> UserInRoleModels { get; set; }

        public UserInRoleResponseModel()
        {
            UserInRoleModels = new List<UserInRoleModel>();
        }
    }
}
