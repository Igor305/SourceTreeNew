﻿using EducationApp.BusinessLogicLayer.Models.ResponseModels.Base;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.Account
{
    public class LoginAccountResponseModel : BaseResponseModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
