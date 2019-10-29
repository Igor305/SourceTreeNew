using AutoMapper;
using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Order;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IPaymentRepository paymentRepository, IMapper mapper, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _paymentRepository = paymentRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<OrderResponseModel> GetAllIsDeleted()
        {
            List<Order> orders = await _orderRepository.GetAllIsDeleted();
            List<OrderModel> orderModels = _mapper.Map<List<Order>, List<OrderModel>>(orders);
            foreach (OrderModel orderModel in orderModels)
            {
                Payment payment = await _paymentRepository.GetById(orderModel.PaymentId);
                PaymentModel paymentModels = _mapper.Map<Payment, PaymentModel>(payment);
                List<OrderItemModel> orderItemModels = _mapper.Map<List<OrderItem>, List<OrderItemModel>>(orderModel.OrderItems);
            }
            OrderResponseModel orderResponseModel = new OrderResponseModel();
            orderResponseModel.orderModels = orderModels;
            orderResponseModel.Messege = "Successfully";
            orderResponseModel.Status = true;
            return orderResponseModel;
        }
        public async Task<OrderResponseModel> GetAll()
        {
            List<Order> orders = await _orderRepository.GetAll();
            List<OrderModel> orderModels = _mapper.Map<List<Order>, List<OrderModel>>(orders);
            foreach (OrderModel orderModel in orderModels)
            {
                Payment payment = await _paymentRepository.GetById(orderModel.PaymentId);
                PaymentModel paymentModels = _mapper.Map<Payment, PaymentModel>(payment);
                List<OrderItemModel> orderItemModels = _mapper.Map<List<OrderItem>, List<OrderItemModel>>(orderModel.OrderItems);
            }
            OrderResponseModel orderResponseModel = new OrderResponseModel();
            orderResponseModel.orderModels = orderModels;
            orderResponseModel.Messege = "Successfully";
            orderResponseModel.Status = true;
            return orderResponseModel;
        }
        public async Task<OrderResponseModel> Pagination(PaginationPageOrderModel paginationPageOrderModel)
        {
            OrderResponseModel orderResponseModel = new OrderResponseModel();
            if (paginationPageOrderModel.Take == 0)
            {
                orderResponseModel.Messege = "Error";
                orderResponseModel.Status = false;
                orderResponseModel.Error.Add("Take is null");
            }
            if (paginationPageOrderModel.Skip < 0)
            {
                orderResponseModel.Messege = "Error";
                orderResponseModel.Status = false;
                orderResponseModel.Error.Add("Skip < 0");
            }
            if (paginationPageOrderModel.Take < 0)
            {
                orderResponseModel.Messege = "Error";
                orderResponseModel.Status = false;
                orderResponseModel.Error.Add("Take < 0");
            }
            if (orderResponseModel.Messege == null)
            {
                IQueryable<Order> pagination = _orderRepository.Pagination();
                int count = pagination.Count();
                List<Order> items = pagination.Skip(paginationPageOrderModel.Skip).Take(paginationPageOrderModel.Take).ToList();

                PaginationOrderModel paginationOrderModel = new PaginationOrderModel(count, paginationPageOrderModel.Skip, paginationPageOrderModel.Take);
                IndexViewModel viewModel = new IndexViewModel
                {
                    PaginationOrderModel = paginationOrderModel,
                    Orders = items
                };              
                List<OrderModel> orderModels = _mapper.Map<List<Order>, List<OrderModel>>(items);
                foreach (OrderModel orderModel in orderModels)
                {
                    Payment payment = await _paymentRepository.GetById(orderModel.PaymentId);
                    PaymentModel paymentModels = _mapper.Map<Payment, PaymentModel>(payment);
                    List<OrderItemModel> orderItemModels = _mapper.Map<List<OrderItem>, List<OrderItemModel>>(orderModel.OrderItems);
                }
                orderResponseModel.Messege = "Successfully";
                orderResponseModel.Status = true;
                orderResponseModel.orderModels = orderModels;
            }
            return orderResponseModel;
        }
        public async Task<OrderResponseModel> GetById(GetByIdOrderModel getByIdOrderModel)
        {
            OrderResponseModel orderResponseModel = new OrderResponseModel();
            Order order = await _orderRepository.GetById(getByIdOrderModel.Id);
            if (order == null)
            {
                orderResponseModel.Messege = "Error";
                orderResponseModel.Status = false;
                orderResponseModel.Error.Add("This OrderId is not in database");
            }
            Payment payment = await _paymentRepository.GetById(order.PaymentId);
            if (payment == null)
            {
                orderResponseModel.Messege = "Error";
                orderResponseModel.Status = false;
                orderResponseModel.Error.Add("This PaymentId is not in database");
            }
            User user = await _userRepository.GetById(order.UserId);
            if (user == null)
            {
                orderResponseModel.Messege = "Error";
                orderResponseModel.Status = false;
                orderResponseModel.Error.Add("This UserId is not in database");
            }
            if (orderResponseModel.Messege == null)
            {
                OrderModel orderModel = _mapper.Map<Order, OrderModel>(order);
                PaymentModel paymentModel = _mapper.Map<Payment, PaymentModel>(payment);
                List<OrderItemModel> orderItemModel = _mapper.Map<List<OrderItem>, List<OrderItemModel>>(order.OrderItem);
                orderResponseModel.orderModels.Add(orderModel);
                orderResponseModel.paymentModels.Add(paymentModel);
                orderResponseModel.orderItemModels = orderItemModel;
                orderResponseModel.Messege = "Successfully";
                orderResponseModel.Status = true;
            }
            return orderResponseModel;      
        }
        public async Task<OrderResponseModel> Create(CreateOrderModel createOrderModel)
        {
            OrderResponseModel orderResponseModel = new OrderResponseModel();

            if (createOrderModel.UserId == null)
            {
                orderResponseModel.Messege = "Error";
                orderResponseModel.Status = false;
                orderResponseModel.Error.Add("UserId is not null");
            }
            if (createOrderModel.PaymentId == null)
            {
                orderResponseModel.Messege = "Error";
                orderResponseModel.Status = false;
                orderResponseModel.Error.Add("PaymentId is not null");
            }
            if (orderResponseModel.Messege == null)
            {
                Order order = _mapper.Map<CreateOrderModel, Order>(createOrderModel);
                Payment payment = await _paymentRepository.GetById(order.PaymentId);
                if (payment == null)
                {
                    orderResponseModel.Messege = "Error";
                    orderResponseModel.Status = false;
                    orderResponseModel.Error.Add("This PaymentId is not in database");
                }
                User user = await _userRepository.GetById(order.UserId);
                if (user == null)
                {
                    orderResponseModel.Messege = "Error";
                    orderResponseModel.Status = false;
                    orderResponseModel.Error.Add("This UserId is not in database");
                }
                List<CreateOrderItemModel> createOrderItemModels = new List<CreateOrderItemModel>();
                foreach (CreateOrderItemModel createOrderItemModel in createOrderItemModels)
                {
                    if (createOrderItemModel.PrintingEditionId == null)
                    {
                        orderResponseModel.Messege = "Error";
                        orderResponseModel.Status = false;
                        orderResponseModel.Error.Add("This PrintingEditionId is not in database");
                    }
                    if (createOrderItemModel.OrderId == null)
                    {
                        orderResponseModel.Messege = "Error";
                        orderResponseModel.Status = false;
                        orderResponseModel.Error.Add("This OrderId is not in database");
                    }
                    if (createOrderItemModel.UnitPrice <= 0)
                    {
                        orderResponseModel.Messege = "Error";
                        orderResponseModel.Status = false;
                        orderResponseModel.Error.Add("This UnitPrice <= 0");
                    }
                    if (createOrderItemModel.Currency <= 0)
                    {
                        orderResponseModel.Messege = "Error";
                        orderResponseModel.Status = false;
                        orderResponseModel.Error.Add("This Currency <= 0");
                    }
                    if (createOrderItemModel.Amount <= 0)
                    {
                        orderResponseModel.Messege = "Error";
                        orderResponseModel.Status = false;
                        orderResponseModel.Error.Add("This Amount <= 0");
                    }
                }
                if (orderResponseModel.Messege == null)
                {
                    order.CreateDateTime = DateTime.Now;
                    order.UpdateDateTime = DateTime.Now;
                    await _orderRepository.Create(order);
                    await _paymentRepository.Create(payment);
                    foreach (CreateOrderItemModel createOrderItemModel in createOrderItemModels)
                    {
                        OrderItem orderItem = _mapper.Map<CreateOrderItemModel, OrderItem>(createOrderItemModel);
                        await _orderItemRepository.Create(orderItem);
                    }
                    OrderModel orderModel = _mapper.Map<Order, OrderModel>(order);
                    PaymentModel paymentModel = _mapper.Map<Payment, PaymentModel>(payment);
                    List<OrderItemModel> orderItemModels = _mapper.Map<List<OrderItem>, List<OrderItemModel>>(order.OrderItem);
                    orderResponseModel.orderModels.Add(orderModel);
                    orderResponseModel.paymentModels.Add(paymentModel);
                    orderResponseModel.orderItemModels = orderItemModels;
                    orderResponseModel.Messege = "Successfully";
                    orderResponseModel.Status = true;
                }
            }
            return orderResponseModel;
        }
        public async Task<OrderResponseModel> Update(UpdateOrderModel updateOrderModel)
        {
            OrderResponseModel orderResponseModel = new OrderResponseModel();
            if (updateOrderModel.Id == null)
            {
                orderResponseModel.Messege = "Error";
                orderResponseModel.Status = false;
                orderResponseModel.Error.Add("This OrderId is not null");
            }
            if (updateOrderModel.UserId == null)
            {
                orderResponseModel.Messege = "Error";
                orderResponseModel.Status = false;
                orderResponseModel.Error.Add("UserId is not null");
            }
            if (updateOrderModel.PaymentId == null)
            {
                orderResponseModel.Messege = "Error";
                orderResponseModel.Status = false;
                orderResponseModel.Error.Add("PaymentId is not null");
            }
            if (orderResponseModel.Messege == null)
            {
                Order order = new Order();
                 _mapper.Map(updateOrderModel, order);
                Payment payment = await _paymentRepository.GetById(order.PaymentId);
                if (payment == null)
                {
                    orderResponseModel.Messege = "Error";
                    orderResponseModel.Status = false;
                    orderResponseModel.Error.Add("This PaymentId is not in database");
                }
                User user = await _userRepository.GetById(order.UserId);
                if (user == null)
                {
                    orderResponseModel.Messege = "Error";
                    orderResponseModel.Status = false;
                    orderResponseModel.Error.Add("This UserId is not in database");
                }
                List<UpdateOrderItemModel> updateOrderItemModels = new List<UpdateOrderItemModel>();
                foreach (UpdateOrderItemModel updateOrderItemModel in updateOrderItemModels)
                {
                    if (updateOrderItemModel.Id == null)
                    {
                        orderResponseModel.Messege = "Error";
                        orderResponseModel.Status = false;
                        orderResponseModel.Error.Add("This OrderItemId is not null");
                    }
                    if (updateOrderItemModel.PrintingEditionId == null)
                    {
                        orderResponseModel.Messege = "Error";
                        orderResponseModel.Status = false;
                        orderResponseModel.Error.Add("This PrintingEditionId is not in database");
                    }
                    if (updateOrderItemModel.OrderId == null)
                    {
                        orderResponseModel.Messege = "Error";
                        orderResponseModel.Status = false;
                        orderResponseModel.Error.Add("This OrderId is not in database");
                    }
                    if (updateOrderItemModel.UnitPrice <= 0)
                    {
                        orderResponseModel.Messege = "Error";
                        orderResponseModel.Status = false;
                        orderResponseModel.Error.Add("This UnitPrice <= 0");
                    }
                    if (updateOrderItemModel.Currency <= 0)
                    {
                        orderResponseModel.Messege = "Error";
                        orderResponseModel.Status = false;
                        orderResponseModel.Error.Add("This Currency <= 0");
                    }
                    if (updateOrderItemModel.Amount <= 0)
                    {
                        orderResponseModel.Messege = "Error";
                        orderResponseModel.Status = false;
                        orderResponseModel.Error.Add("This Amount <= 0");
                    }
                }
                if (orderResponseModel.Messege == null)
                {
                    foreach (UpdateOrderItemModel updateOrderItemModel in updateOrderItemModels)
                    {
                        OrderItem orderItem = _mapper.Map<UpdateOrderItemModel, OrderItem>(updateOrderItemModel);
                        await _orderItemRepository.Update(orderItem);
                    }
                    order.UpdateDateTime = DateTime.Now;
                    await _orderRepository.Update(order);
                    await _paymentRepository.Update(payment);
                    OrderModel orderModel = _mapper.Map<Order, OrderModel>(order);
                    PaymentModel paymentModel = _mapper.Map<Payment, PaymentModel>(payment);
                    List<OrderItemModel> orderItemModels = _mapper.Map<List<OrderItem>, List<OrderItemModel>>(order.OrderItem);
                    orderResponseModel.orderModels.Add(orderModel);
                    orderResponseModel.paymentModels.Add(paymentModel);
                    orderResponseModel.orderItemModels = orderItemModels;
                    orderResponseModel.Messege = "Successfully";
                    orderResponseModel.Status = true;
                }
            }
            return orderResponseModel;
        }
        public async Task<OrderResponseModel> Delete(DeleteOrderModel deleteOrderModel)
        {
            OrderResponseModel orderResponseModel = new OrderResponseModel();
            if (deleteOrderModel.Id == null)
            {
                orderResponseModel.Messege = "Error";
                orderResponseModel.Status = false;
                orderResponseModel.Error.Add("This OrderId is not null");
            }
            Order order = await _orderRepository.GetById(deleteOrderModel.Id);
            if (order == null)
            {
                orderResponseModel.Messege = "Error";
                orderResponseModel.Status = false;
                orderResponseModel.Error.Add("This OrderId is not in database");
            }
            if (orderResponseModel.Messege == null)
            {
                order.IsDeleted = true;
                await _orderRepository.Update(order);
            }
            return orderResponseModel;
        }
    }
}