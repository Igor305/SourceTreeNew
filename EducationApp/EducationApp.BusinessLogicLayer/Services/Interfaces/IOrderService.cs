using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Order;
using System;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseModel> GetAll();
        Task<OrderResponseModel> GetAllWithoutRemove();
        Task<OrderResponseModel> Pagination(PaginationOrderModel paginationOrderModel);
        Task<OrderResponseModel> GetById(Guid id);
        Task<OrderModel> Create(CreateOrderModel createOrderModel);
        Task<OrderResponseModel> Delete(Guid id);
    }
}
