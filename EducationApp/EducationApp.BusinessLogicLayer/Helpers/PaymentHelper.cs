using Stripe;

namespace EducationApp.BusinessLogicLayer.Helpers
{
    public class PaymentHelper
    {
        public string Charge(string email, string source, string Description, string currency, long amount)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();
            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = email,
                Source = source
            });

            var charge = charges.Create(new ChargeCreateOptions
            {

                Amount = amount,
                Description = Description,
                Currency = currency,
                Customer = customer.Id
            });
            if (charge.Status == "succeeded")
            {
                string BalanceTransactionId = charge.BalanceTransactionId;
                return BalanceTransactionId;
            }
            return "Что-то не так";
        }
    }
}

