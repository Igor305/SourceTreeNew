using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Order;
using EducationApp.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseModel> GetAllIsDeleted();
        Task<OrderResponseModel> GetAll();
        Task<OrderResponseModel> Pagination(PaginationPageOrderModel paginationPageOrderModel);
        Task<OrderResponseModel> GetById(GetByIdOrderModel getByIdOrderModel);
        Task<OrderResponseModel> Create(CreateOrderModel createOrderModel);
        Task<OrderResponseModel> Update(UpdateOrderModel updateOrderModel);
        Task<OrderResponseModel> Delete(DeleteOrderModel deleteOrderModel);
    }
}
