using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Order;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<OrderResponseModel> GetAllIsDeleted()
        {
            OrderResponseModel orderResponseModel = await _orderService.GetAllIsDeleted();
            return orderResponseModel;
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
        public async Task<OrderResponseModel> GetAll()
        {
            OrderResponseModel orderResponseModel = await _orderService.GetAll();
            return orderResponseModel;
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
        public async Task<OrderResponseModel> Pagination([FromQuery] PaginationPageOrderModel paginationPageOrderModel)
        {
            OrderResponseModel orderResponseModel = new OrderResponseModel();
            if (ModelState.IsValid)
            {
                orderResponseModel = await _orderService.Pagination(paginationPageOrderModel);
                return orderResponseModel;
            }
            orderResponseModel.Messege = "Error";
            orderResponseModel.Status = false;
            orderResponseModel.Error.Add("Post, not valide");
            return orderResponseModel;
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
        public async Task<OrderResponseModel> Create([FromBody]CreateOrderModel createOrderModel)
        {
            OrderResponseModel orderResponseModel = await _orderService.Create(createOrderModel);
            return orderResponseModel;
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
        public async Task<OrderResponseModel> Update([FromBody]UpdateOrderModel updateOrderModel)
        {
            OrderResponseModel orderResponseModel = await _orderService.Update(updateOrderModel);
            return orderResponseModel;
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
        public async Task<OrderResponseModel> Delete([FromBody]DeleteOrderModel deleteOderModel)
        {
            OrderResponseModel orderResponseModel = await _orderService.Delete(deleteOderModel);
            return orderResponseModel;
        }
    }
}
