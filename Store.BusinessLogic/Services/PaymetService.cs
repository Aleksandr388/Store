using AutoMapper;
using Microsoft.Extensions.Options;
using Shared.Constants;
using Shared.Enums;
using Store.BusinessLogic.Common;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Payments;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAcess.Entities;
using Store.DataAcess.Models;
using Store.DataAcess.Repositories.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class PaymetService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly StripeSettings _stripeSettings;
        private readonly IPrintingEditionRepository _printingEditionRepository;

        public PaymetService(IMapper mapper, IOptions<StripeSettings> options, IPaymentRepository paymentRepository, IOrderRepository orderRepository, IPrintingEditionRepository printingEditionRepository)
        {
            _printingEditionRepository = printingEditionRepository;
            _stripeSettings = options.Value;
            _mapper = mapper;
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
        }

        public async Task CreateOrderAsync(OrderModel model)
        {
            var orderModel = _mapper.Map<DataAcess.Entities.Order>(model);

            await _orderRepository.CreateAsync(orderModel);
        }

        public async Task CreateOrderItemAsync(List<OrderItemModel> orderItems)
        {
            var order = await _orderRepository.GetByIdAsync(orderItems.First().OrderId);

            if (order is null || order.IsRemoved || order.OrderStatus.Equals(OrderStatus.Paid))
            {
                throw new Exception();
            }

            var itemsId = orderItems.Select(x => x.PrintingEditionId).ToList();
            var printingEditions = (await _printingEditionRepository.GetEditionRangeAsync(itemsId)).Select(x => x.Id);

            if (!printingEditions.Any())
            {
                throw new Exception();
            }

            orderItems.RemoveAll(x => !printingEditions.Contains(x.PrintingEditionId));

            var newOrderItems = _mapper.Map<List<DataAcess.Entities.OrderItem>>(orderItems);

            var prices = await _printingEditionRepository.GetPrices(newOrderItems);

            foreach (var item in newOrderItems)
            {
                item.Amount = prices.Where(x => x.Id == item.PrintingEditionId).FirstOrDefault().Price;
            }

            order.OrderItems.AddRange(newOrderItems);

            await _orderRepository.SaveChagesAsync();
        }

        public async Task CreatePaymentAsync(PayModel model)
        {
            var order = await _orderRepository.GetByIdAsync(model.OrderId);
            var orderPrice = _orderRepository.GetOrderPrice(order);

            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            TokenCardOptions tokenCard = new TokenCardOptions
            {
                Name = model.Name,
                Number = model.Number,
                ExpYear = model.ExpYear,
                ExpMonth = model.ExpMonth,
                Cvc = model.Cvc,
            };

            var tokenOptions = new TokenCreateOptions
            {
                Card = tokenCard
            };

            var tokenService = new TokenService();
            var token = tokenService.Create(tokenOptions);
            var customer = new CustomerCreateOptions
            {
                Email = model.Email,
                Source = token.Id
            };

            var custSercise = new CustomerService();
            var stpCustomer = custSercise.Create(customer);
            var options = new ChargeCreateOptions
            {
                Currency = Curency.USD.ToString(),
                ReceiptEmail = DefaultValues.TestEmailForPay,
                Description = model.Description,
                Customer = stpCustomer.Id,
                Amount = (long)orderPrice * DefaultValues.DefaultAmountValue
            };

            var service = new ChargeService();
            var charge = service.Create(options);
            var payment = new PaymentModel
            {
                TransactionId = charge.Id
            };

            var newPayment = _mapper.Map<Payment>(payment);

            await _paymentRepository.CreateAsync(newPayment);

            var paymentId = await _paymentRepository.GetByIdAsync(newPayment.Id);

            order.OrderStatus = OrderStatus.Paid;
            order.PaymentId = paymentId.Id;

            await _orderRepository.UpdateAsync(order);
        }
    }
}
