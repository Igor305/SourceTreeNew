using EducationApp.BusinessLogicLayer.Models.ResponseModels.Base;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.Order
{
    public class OrderResponseModel : BaseResponseModel
    {
        public List<OrderModel> orderModels = new List<OrderModel>();

        public OrderResponseModel()
        {
            orderModels = new List<OrderModel>();
        }
    }
}
