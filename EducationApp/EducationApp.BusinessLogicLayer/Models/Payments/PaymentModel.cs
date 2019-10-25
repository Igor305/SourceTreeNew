namespace EducationApp.BusinessLogicLayer.Models.Payments
{
    public class PaymentModel
    {
        public string Email { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public long Amount { get; set; }

    }
}
