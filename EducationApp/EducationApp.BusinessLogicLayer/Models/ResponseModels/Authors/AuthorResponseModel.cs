using EducationApp.BusinessLogicLayer.Models.ResponseModels.Base;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.Authors
{
    public class AuthorResponseModel : BaseResponseModel
    {
        public List<AuthorModel> AuthorModel { get; set; }

        public AuthorResponseModel()
        {
            AuthorModel = new List<AuthorModel>();
        }
    }
}

