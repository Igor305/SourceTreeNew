using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Order;
using EducationApp.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseModel> GetAll();
        Task<OrderResponseModel> GetAllWithoutRemove();
        Task<OrderResponseModel> Pagination(PaginationOrderModel paginationOrderModel);
        Task<OrderResponseModel> GetById(Guid id);
        Task<OrderResponseModel> Create(CreateOrderModel createOrderModel);
        Task<OrderResponseModel> Update(Guid id, CreateOrderModel createOrderModel);
        Task<OrderResponseModel> Delete(Guid id);
    }
}
