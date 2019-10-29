using EducationApp.BusinessLogicLayer.Models.Payments;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Order;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IPaymentService
    {
        void CreateTransaction(PaymentModel paymentModel);
    }
}
