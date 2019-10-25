using EducationApp.BusinessLogicLayer.Models.Payments;
using EducationApp.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IPaymentService
    {
        List<Payment> GetAllIsDeleted();
        List<Payment> GetAll();
        void CreateTransaction(PaymentModel paymentModel);
        void Update(UpdatePaymentModel updatePaymentModel);
        void Delete(DeletePaymentModel deletePaymentModel);
    }
}
