using EducationApp.BusinessLogicLayer.Models.ResponseModels.Base;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Models.ResponseModels.Order
{
    public class OrderResponseModel : BaseResponseModel
    {
        public List<OrderModel> orderModels = new List<OrderModel>();
        public List<OrderItemModel> orderItemModels = new List<OrderItemModel>();
        public List<PaymentModel> paymentModels = new List<PaymentModel>();

        public OrderResponseModel()
        {
            orderModels = new List<OrderModel>();
            orderItemModels = new List<OrderItemModel>();
            paymentModels = new List<PaymentModel>();
        }
    }
}
