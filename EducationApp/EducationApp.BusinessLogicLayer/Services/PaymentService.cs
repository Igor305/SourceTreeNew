using EducationApp.BusinessLogicLayer.Helpers;
using EducationApp.BusinessLogicLayer.Models.Payments;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public List<Payment> GetAllIsDeleted()
        {
            var allIsDeleted = _paymentRepository.GetAllIsDeleted();
            return allIsDeleted;
        }
        public List<Payment> GetAll()
        {
            var all = _paymentRepository.GetAll();
            return all;
        }
        public void CreateTransaction(PaymentModel paymentModel)
        {
            PaymentHelper paymentHelper = new PaymentHelper();
            string transactionId = paymentHelper.Charge(paymentModel.Email, paymentModel.Source, paymentModel.Description, paymentModel.Currency, paymentModel.Amount);
            Payment payment = new Payment();
            payment.TransactionId = transactionId;
            payment.CreateDateTime = DateTime.Now;
            payment.UpdateDateTime = DateTime.Now;
            _paymentRepository.Create(payment);
        }
        public void Update(UpdatePaymentModel updatePaymentModel)
        {
            var all = _paymentRepository.GetAll();
            var findPayment = all.Find(x => x.Id == updatePaymentModel.Id);
            findPayment.TransactionId = updatePaymentModel.TransactionId;
            findPayment.UpdateDateTime = DateTime.Now;
            _paymentRepository.Update(findPayment);
        }
        public void Delete(DeletePaymentModel deletePaymentModel)
        {
            var all = _paymentRepository.GetAll();
            var findPayment = all.Find(x => x.Id == deletePaymentModel.Id);
            findPayment.IsDeleted = true;
            _paymentRepository.Update(findPayment);

        }
    }
}
