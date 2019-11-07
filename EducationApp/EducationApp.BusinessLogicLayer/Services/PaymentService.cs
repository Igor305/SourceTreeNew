using EducationApp.BusinessLogicLayer.Models.ResponseModels.Order;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Repositories.Interfaces;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public void CreateTransaction(PaymentModel paymentModel)
        {
           /* PaymentHelper paymentHelper = new PaymentHelper();
            string transactionId = paymentHelper.Charge(paymentModel.Email, paymentModel.Source, paymentModel.Description, paymentModel.Currency, paymentModel.Amount);
            Payment payment = new Payment();
            payment.TransactionId = transactionId;
            payment.CreateDateTime = DateTime.Now;
            payment.UpdateDateTime = DateTime.Now;
            _paymentRepository.Create(payment);*/
        }
    }
}
