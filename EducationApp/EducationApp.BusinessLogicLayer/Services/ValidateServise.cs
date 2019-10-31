using EducationApp.BusinessLogicLayer.Models.ResponseModels;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Base;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class ValidateServise<T> where T : BaseResponseModel
    {
        ResponseConstants responseConstants = new ResponseConstants();
        public T Successfully(T t)
        {
            t.Messege = responseConstants.Successfully;
            t.Status = true;
            return t;
        }
        public T WarningOrErro(T t, string warninOrError)
        {
            t.Messege = responseConstants.Successfully;
            t.Status = false;
            t.Error.Add(warninOrError);
            return t;
        }
    }
}
