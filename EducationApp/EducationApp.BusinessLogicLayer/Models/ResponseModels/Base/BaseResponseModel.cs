using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.Base
{
    public class BaseResponseModel
    {
        public string Messege { get; set; }
        public bool Status { get; set; }
        public List<string> Warning { get; set; }
        public List<string> Error { get; set; }

        public BaseResponseModel()
        {
            Warning = new List<string>();
            Error = new List<string>();
        }
    }
}
