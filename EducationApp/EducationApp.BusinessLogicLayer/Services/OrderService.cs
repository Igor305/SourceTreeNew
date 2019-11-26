using AutoMapper;
using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Models.ResponseModels;
using EducationApp.BusinessLogicLayer.Models.ResponseModels.Order;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Entities.Enum;
using EducationApp.DataAccessLayer.Repositories.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Order = EducationApp.DataAccessLayer.Entities.Order;
using OrderItem = EducationApp.DataAccessLayer.Entities.OrderItem;

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
            OrderResponseModel orderResponseModel = ValidateGetAll();
            orderResponseModel.orderModels = orderModels;

            return orderResponseModel;
        }

        public async Task<OrderResponseModel> GetAllWithoutRemove()
        {
            List<Order> orders = await _orderRepository.GetAllWithoutRemove();
            List<OrderModel> orderModels = _mapper.Map<List<Order>, List<OrderModel>>(orders);
            OrderResponseModel orderResponseModel = ValidateGetAll();
            orderResponseModel.orderModels = orderModels;
            return orderResponseModel;
        }

        private OrderResponseModel ValidateGetAll()
        {
            OrderResponseModel orderResponseModel = new OrderResponseModel();

            orderResponseModel.Status = true;
            orderResponseModel.Message = ResponseConstants.Successfully;

            return orderResponseModel;
        }

        public async Task<OrderResponseModel> Pagination(PaginationOrderModel paginationOrderModel)
        {
            OrderResponseModel orderResponseModel = ValidatePagination(paginationOrderModel);
            
            if (orderResponseModel.Status)
            {
                List<Order> pagination = await _orderRepository.Pagination(paginationOrderModel.Skip, paginationOrderModel.Take);            
                List<OrderModel> orderModels = _mapper.Map<List<Order>, List<OrderModel>>(pagination);
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
            orderResponseModel.Message = ResponseConstants.Successfully;

            return orderResponseModel;
        }

        public async Task<OrderResponseModel> GetById(Guid id)
        {
            OrderResponseModel orderResponseModel = await ValidateGetById(id);

            if (orderResponseModel.Status)
            {
                Order order = await _orderRepository.GetById(id);            
                OrderModel orderModel = _mapper.Map<Order, OrderModel>(order);
                orderResponseModel.orderModels.Add(orderModel);
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
            orderResponseModel.Message = orderResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

            return orderResponseModel;
        }

        public async Task<OrderModel> Create(CreateOrderModel createOrderModel)
        {
            Order order = new Order();
            OrderItem orderItem = new OrderItem();
            long totalAmount = 0;
            foreach (OrderPrintingEdition orderPrintingEdition in createOrderModel.OrderPrintingEditions)
            {
                PrintingEdition printingEdition = await _printingEditionRepository.GetById(orderPrintingEdition.Id);

                orderItem.Count = orderPrintingEdition.Count;
                orderItem.UnitPrice = printingEdition.Price;
                orderItem.Amount = orderItem.Count * orderItem.UnitPrice;
                totalAmount =+ orderItem.Amount;

            }
            User user = await _userRepository.GetById(createOrderModel.UserId);
            Payment payment = new Payment();

            string transactionId = PaymentCharge(user.Email, createOrderModel.TypeOfPaymentCard, createOrderModel.Description, createOrderModel.Currency, totalAmount);
            payment.TransactionId = transactionId;
            payment.CreateDateTime = DateTime.Now;
            payment.UpdateDateTime = DateTime.Now;
            await _paymentRepository.Create(payment);

            order.UserId = user.Id;
            order.Description = createOrderModel.Description;
            order.PaymentId = payment.Id;
            order.CreateDateTime = DateTime.Now;
            order.UpdateDateTime = DateTime.Now;
            await _orderRepository.Create(order);
            foreach (OrderPrintingEdition orderPrintingEdition in createOrderModel.OrderPrintingEditions)
            {
                PrintingEdition printingEdition = await _printingEditionRepository.GetById(orderPrintingEdition.Id);

                orderItem.OrderId = order.Id;
                orderItem.Count = orderPrintingEdition.Count;
                orderItem.PrintingEditionId = printingEdition.Id;
                orderItem.UnitPrice = printingEdition.Price;
                orderItem.Currency = printingEdition.Currency;
                orderItem.Amount = orderItem.Count * orderItem.UnitPrice;
                await _orderItemRepository.Create(orderItem);
                order.OrderItems.Add(orderItem);
            }
                OrderModel orderModel = _mapper.Map<Order, OrderModel>(order);
            return orderModel;
        }

        public string PaymentCharge(string email, string source, string Description, Currency currency, long amount)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();
            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = email,
                Source = source
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = amount,
                Description = Description,
                Currency = currency.ToString(),
                Customer = customer.Id
            });
            if (charge.Status == ResponseConstants.Successfully)
            {
                string BalanceTransactionId = charge.BalanceTransactionId;
                return BalanceTransactionId;
            }
            return ResponseConstants.Error;
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
            orderResponseModel.Message = orderResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

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
            orderResponseModel.Message = orderResponseModel.Status ? ResponseConstants.Successfully : ResponseConstants.Error;

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