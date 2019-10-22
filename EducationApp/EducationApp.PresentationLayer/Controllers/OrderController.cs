using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EducationApp.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        /// <summary>
        /// Get all Order (IsDeleted = true)
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
            var allIsDeleted = _orderService.GetAllIsDeleted();
            return allIsDeleted;
        }
        /// <summary>
        /// Get all Order
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
            var all = _orderService.GetAll();
            return all;
        }
        /// <summary>
        /// Get Pagination Order
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/Pagination
        ///     "Skip":"1",
        ///     "Take":"2"
        ///
        /// </remarks>
        [HttpGet("Pagination")]
        public object Pagination([FromQuery] PaginationPageOrderModel paginationPageOrderModel)
        {
            if (ModelState.IsValid)
            {
                var filter = _orderService.Pagination(paginationPageOrderModel);
                return filter;
            }
            return "Модель не валидная(";
        }
        /// <summary>
        /// Create new Order
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST/Create
        ///     {
        ///         "Description": "Выгодный товар"
        ///     }
        ///
        /// </remarks>
        [HttpPost("Create")]
        public string Create([FromBody]CreateOrderModel createOrderModel)
        {
            string create = _orderService.Create(createOrderModel);
            return create;
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
        ///         "Description": "Выгодный товар"
        ///     }
        ///
        /// </remarks>
        [HttpPut("Update")]
        public string Update([FromBody]UpdateOrderModel updateOrderModel)
        {
            string update = _orderService.Update(updateOrderModel);
            return update;
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
        public string Delete([FromBody]DeleteOderModel deleteOderModel)
        {
            string delete = _orderService.Delete(deleteOderModel);
            return delete;
        }
    }
}
