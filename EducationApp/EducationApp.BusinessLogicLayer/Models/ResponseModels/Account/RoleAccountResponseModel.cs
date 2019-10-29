using EducationApp.BusinessLogicLayer.Models.ResponseModels.Base;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.Account
{
    public class RoleAccountResponseModel : BaseResponseModel
    {
        public List<RoleAccountModel> roleAccountModels { get; set; }

        public RoleAccountResponseModel()
        {
            roleAccountModels = new List<RoleAccountModel>();
        }
    }
}
