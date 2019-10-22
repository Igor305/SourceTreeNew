using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<Order> GetAllIsDeleted()
        {
            var allIsDeleted = _orderRepository.GetAllIsDeleted();
            return allIsDeleted;
        }
        public List<Order> GetAll()
        {
            var all = _orderRepository.GetAll();
            return all;
        }
        public List<Order> Pagination(PaginationPageOrderModel paginationPageOrderModel)
        {
            if (paginationPageOrderModel.Skip < 1)
            {
                paginationPageOrderModel.Skip = 1;
            }
            var pagination = _orderRepository.Pagination();
            var count = pagination.Count();
            var items = pagination.Skip(paginationPageOrderModel.Skip).Take(paginationPageOrderModel.Take).ToList();

            PaginationOrderModel paginationOrderModel = new PaginationOrderModel(count, paginationPageOrderModel.Skip, paginationPageOrderModel.Take);
            IndexViewModel viewModel = new IndexViewModel
            {
                PaginationOrderModel = paginationOrderModel,
                Orders = items
            };
            return items;
        }
        public string Create(CreateOrderModel createOrderModel)
        {
            Order order = new Order();
            order.Description = createOrderModel.Description;
            order.CreateDateTime = DateTime.Now;
            order.UpdateDateTime = DateTime.Now;
            _orderRepository.Create(order);
            return "Добавлена новая запись";
        }
        public string Update(UpdateOrderModel updateOrderModel)
        {
            var all = _orderRepository.GetAll();
            var findOrder = all.Find(x => x.Id == updateOrderModel.Id);
            findOrder.Description = updateOrderModel.Description;
            findOrder.UpdateDateTime = DateTime.Now;
            _orderRepository.Update(findOrder);
            return "Запись Обновлена";
        }
        public string Delete(DeleteOderModel deleteOderModel)
        {
            var all = _orderRepository.GetAll();
            var findOrder = all.Find(x => x.Id == deleteOderModel.Id);
            findOrder.IsDeleted = true;
            _orderRepository.Update(findOrder);
            return "Запись удалена";
        }
    }
}
