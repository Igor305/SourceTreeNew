using EducationApp.BusinessLogicLayer.Models.Payments;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EducationApp.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        /// <summary>
        /// Get all Payment (IsDeleted = true)
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/GetAllIsDeleted
        ///
        /// </remarks>
        [HttpGet("GetAllIsDeleted")]
        public object GetAllIsDeleted()
        {
            var allIsDeleted = _paymentService.GetAllIsDeleted();
            return allIsDeleted;
        }
        /// <summary>
        /// Get all Payment 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/GetAll
        ///
        /// </remarks>
        [HttpGet("GetAll")]
        public object GetAll()
        {
            var all = _paymentService.GetAll();
            return all;
        }
        /// <summary>
        /// Create new Payment
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post/Create
        ///     {
        ///         "Email": "",
        ///         "Source": "",
        ///         "Description": "Выгодный товар",
        ///         "Currency": "",
        ///         "Amount": ""
        ///     }
        ///
        /// </remarks>
        [HttpPost("Create")]
        public void Create([FromBody]PaymentModel paymentModel)
        {
            _paymentService.CreateTransaction(paymentModel);
        }
        /// <summary>
        /// Update Order for Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT/Update
        ///     {
        ///         "Id": "",
        ///         "TransactionId": "",
        ///     }
        ///
        /// </remarks>
        [HttpPut("Update")]
        public void Update([FromBody]UpdatePaymentModel updatePaymentModel)
        {
            _paymentService.Update(updatePaymentModel);
        }
        /// <summary>
        /// Delete Order for Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE/Delete
        ///     {
        ///         "Id": ""
        ///     }
        ///
        /// </remarks>
        [HttpDelete("Delete")]
        public void Delete([FromBody]DeletePaymentModel deletePaymentModel)
        {
            _paymentService.Delete(deletePaymentModel);
        }
    }
}