using EducationApp.BusinessLogicLayer.Models.ResponseModels.Base;
using System;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.Account
{
    public class AuthAccountResponseModel : BaseResponseModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string PassHash { get; set; }
        public string Role { get; set; }
    }
}
