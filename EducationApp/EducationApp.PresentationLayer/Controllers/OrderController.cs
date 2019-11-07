using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Order;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EducationApp.PresentationLayer.Controllers
{
    /// <summary>
    /// OrderController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderService"></param>
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
        /// Get All Without Remove
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/GetAllWithoutRemove
        ///
        /// </remarks>
        [HttpGet("GetAllWithoutRemove")]
        public async Task<OrderResponseModel> GetAllWithoutRemove()
        {
            OrderResponseModel orderResponseModel = await _orderService.GetAllWithoutRemove();
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
        public async Task<OrderResponseModel> Pagination([FromQuery] PaginationOrderModel paginationOrderModel)
        {
            OrderResponseModel orderResponseModel  = await _orderService.Pagination(paginationOrderModel);
            return orderResponseModel; 
        }
        /// <summary>
        /// Get by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get/GerById
        ///     "Skip":"1",
        ///     "Take":"2"
        ///
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<OrderResponseModel> GetById(Guid id)
        {
            OrderResponseModel orderResponseModel = await _orderService.GetById(id);
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
        public async Task<OrderModel> Create([FromBody]CreateOrderModel createOrderModel)
        {
            OrderModel orderModel = await _orderService.Create(createOrderModel);
            return orderModel;
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
        [HttpDelete("{id}")]
        public async Task<OrderResponseModel> Delete(Guid id)
        {
            OrderResponseModel orderResponseModel = await _orderService.Delete(id);
            return orderResponseModel;
        }
    }
}
