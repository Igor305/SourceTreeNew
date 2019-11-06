using AutoMapper;
using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Models.ResponseModels;
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
        private readonly IPrintingEditionRepository _printingEditionRepository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IPaymentRepository paymentRepository, IMapper mapper, IUserRepository userRepository, IPrintingEditionRepository printingEditionRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _paymentRepository = paymentRepository;
            _userRepository = userRepository;
            _printingEditionRepository = printingEditionRepository;
            _mapper = mapper;
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
            OrderResponseModel orderResponseModel = ValidateGetAll();
            orderResponseModel.orderModels = orderModels;

            return orderResponseModel;
        }

        public async Task<OrderResponseModel> GetAllWithoutRemove()
        {
            List<Order> orders = await _orderRepository.GetAllWithoutRemove();
            List<OrderModel> orderModels = _mapper.Map<List<Order>, List<OrderModel>>(orders);
            foreach (OrderModel orderModel in orderModels)
            {
                Payment payment = await _paymentRepository.GetById(orderModel.PaymentId);
                PaymentModel paymentModels = _mapper.Map<Payment, PaymentModel>(payment);
                List<OrderItemModel> orderItemModels = _mapper.Map<List<OrderItem>, List<OrderItemModel>>(orderModel.OrderItems);
            }
            OrderResponseModel orderResponseModel = ValidateGetAll();
            orderResponseModel.orderModels = orderModels;
            return orderResponseModel;
        }

        private OrderResponseModel ValidateGetAll()
        {
            OrderResponseModel orderResponseModel = new OrderResponseModel();

            orderResponseModel.Status = true;
            orderResponseModel.Messege = ResponseConstants.Successfully;

            return orderResponseModel;
        }

        public async Task<OrderResponseModel> Pagination(PaginationOrderModel paginationOrderModel)
        {
            OrderResponseModel orderResponseModel = ValidatePagination(paginationOrderModel);
            
            if (orderResponseModel.Status)
            {
                List<Order> pagination = await _orderRepository.Pagination(paginationOrderModel.Skip, paginationOrderModel.Take);            
                List<OrderModel> orderModels = _mapper.Map<List<Order>, List<OrderModel>>(pagination);

                foreach (OrderModel orderModel in orderModels)
                {
                    Payment payment = await _paymentRepository.GetById(orderModel.PaymentId);
                    PaymentModel paymentModels = _mapper.Map<Payment, PaymentModel>(payment);
                    List<OrderItemModel> orderItemModels = _mapper.Map<List<OrderItem>, List<OrderItemModel>>(orderModel.OrderItems);
                }
                orderResponseModel.orderModels = orderModels;
            }
            return orderResponseModel;
        }
        
        private OrderResponseModel ValidatePagination (PaginationOrderModel paginationOrderModel)
        {
            OrderResponseModel orderResponseModel = new OrderResponseModel();
            bool isWarning = (paginationOrderModel.Skip < 0 || paginationOrderModel.Take < 0);

            if (isWarning)
            {
                orderResponseModel.Warning.Add(ResponseConstants.LessThanZero);
            }
            orderResponseModel.Status = true;
            orderResponseModel.Messege = ResponseConstants.Successfully;

            return orderResponseModel;
        }

        public async Task<OrderResponseModel> GetById(Guid id)
        {
            OrderResponseModel orderResponseModel = await ValidateGetById(id);

            if (orderResponseModel.Status)
            {
                Order order = await _orderRepository.GetById(id);
                Payment payment = await _paymentRepository.GetById(order.PaymentId);
                OrderModel orderModel = _mapper.Map<Order, OrderModel>(order);
                PaymentModel paymentModel = _mapper.Map<Payment, PaymentModel>(payment);
                List<OrderItemModel> orderItemModel = _mapper.Map<List<OrderItem>, List<OrderItemModel>>(order.OrderItem);
                orderResponseModel.orderModels.Add(orderModel);
                orderResponseModel.paymentModels.Add(paymentModel);
                orderResponseModel.orderItemModels = orderItemModel;
            }
            return orderResponseModel;      
        }

        private async Task<OrderResponseModel> ValidateGetById(Guid id)
        {
            OrderResponseModel orderResponseModel = new OrderResponseModel();
            bool isExist = await _orderRepository.CheckById(id);
            if (!isExist)
            {
                orderResponseModel.Error.Add(ResponseConstants.ErrorId);
            }
            orderResponseModel.Status = isExist;
            orderResponseModel.Messege = orderResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return orderResponseModel;
        }

        public async Task<OrderResponseModel> Create(CreateOrderModel createOrderModel)
        {
            Payment payment = await _paymentRepository.GetById(createOrderModel.PaymentId);
            User user = await _userRepository.GetById(createOrderModel.UserId);
            OrderResponseModel orderResponseModel = ValidateCreateOrder(payment, user);
            if (orderResponseModel.Status)
            {
                Order order = _mapper.Map<CreateOrderModel, Order>(createOrderModel);

                List<CreateOrderItemModel> createOrderItemModels = new List<CreateOrderItemModel>();
                foreach (CreateOrderItemModel createOrderItemModel in createOrderItemModels)
                {
                    PrintingEdition printingEdition = await _printingEditionRepository.GetById(createOrderItemModel.PrintingEditionId);
                    orderResponseModel = ValidateCreateOrderItems(printingEdition, createOrderItemModel);         
                }
                if (orderResponseModel.Status)
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
                }
            }
            return orderResponseModel;
        }

        private OrderResponseModel ValidateCreateOrder(Payment payment, User user)
        {
            OrderResponseModel orderResponseModel = new OrderResponseModel();
            bool isErrorOfNull = payment == null || user == null;

            if (isErrorOfNull)
            {
                orderResponseModel.Error.Add(ResponseConstants.ErrorId);
            }
            orderResponseModel.Status = !isErrorOfNull;
            orderResponseModel.Messege = orderResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return orderResponseModel;
        }

        private OrderResponseModel ValidateCreateOrderItems(PrintingEdition printingEdition, CreateOrderItemModel createOrderItemModel)
        {
            OrderResponseModel orderResponseModel = new OrderResponseModel();
            bool isErrorOfNull = printingEdition == null || createOrderItemModel.OrderId == null;
            bool isErrorOfIncorrectData = createOrderItemModel.Amount <= 0 || createOrderItemModel.Currency <= 0 || createOrderItemModel.UnitPrice <= 0;
            bool isError = isErrorOfNull || isErrorOfIncorrectData;

            if (isErrorOfNull)
            {
                orderResponseModel.Error.Add(ResponseConstants.ErrorId);
            }
            if (isErrorOfIncorrectData)
            {
                orderResponseModel.Error.Add(ResponseConstants.ErrorIncorrectData);
            }
            orderResponseModel.Status = !isError;
            orderResponseModel.Messege = orderResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return orderResponseModel;
        }

        public async Task<OrderResponseModel> Update(Guid id, CreateOrderModel createOrderModel)
        {
            OrderResponseModel orderResponseModel = await ValidateGetById(id);
            if (orderResponseModel.Status)
            {
                Payment payment = await _paymentRepository.GetById(createOrderModel.PaymentId);
                User user = await _userRepository.GetById(createOrderModel.UserId);
                orderResponseModel = ValidateCreateOrder(payment, user);
                if (orderResponseModel.Status)
                {
                    Order order = _mapper.Map<CreateOrderModel, Order>(createOrderModel);

                    List<CreateOrderItemModel> createOrderItemModels = new List<CreateOrderItemModel>();
                    foreach (CreateOrderItemModel createOrderItemModel in createOrderItemModels)
                    {
                        PrintingEdition printingEdition = await _printingEditionRepository.GetById(createOrderItemModel.PrintingEditionId);
                        orderResponseModel = ValidateCreateOrderItems(printingEdition, createOrderItemModel);

                    }
                    if (orderResponseModel.Status)
                    {
                        order.UpdateDateTime = DateTime.Now;
                        await _orderRepository.Update(order);
                        await _paymentRepository.Update(payment);
                        foreach (CreateOrderItemModel createOrderItemModel in createOrderItemModels)
                        {
                            OrderItem orderItem = _mapper.Map<CreateOrderItemModel, OrderItem>(createOrderItemModel);
                            await _orderItemRepository.Update(orderItem);
                        }
                        OrderModel orderModel = _mapper.Map<Order, OrderModel>(order);
                        PaymentModel paymentModel = _mapper.Map<Payment, PaymentModel>(payment);
                        List<OrderItemModel> orderItemModels = _mapper.Map<List<OrderItem>, List<OrderItemModel>>(order.OrderItem);
                        orderResponseModel.orderModels.Add(orderModel);
                        orderResponseModel.paymentModels.Add(paymentModel);
                        orderResponseModel.orderItemModels = orderItemModels;
                    }
                }
            }
            return orderResponseModel;
        }
        public async Task<OrderResponseModel> Delete(Guid id)
        {
            OrderResponseModel orderResponseModel = await ValidateGetById(id);

            if (orderResponseModel.Status)
            {
                Order order = await _orderRepository.GetById(id);
                order.IsDeleted = true;
                await _orderRepository.Update(order);
            }
            return orderResponseModel;
        }
    }
}