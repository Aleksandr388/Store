using AutoMapper;
using Microsoft.Extensions.Options;
using Shared.Constants;
using Shared.Enums;
using Store.BusinessLogic.Common;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Payments;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAcess.Entities;
using Store.DataAcess.Repositories.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class PaymetService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly StripeSettings _stripeSettings;

        public PaymetService( IMapper mapper, IOptions<StripeSettings> options, IPaymentRepository paymentRepository, IOrderRepository orderRepository)
        {
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

        public async Task CreatePaymentAsync(PaymentModel model)
        {
            //var orders = await _orderRepository.GetByIdAsync(model.OrderId);

            //if (orders is null)
            //{
            //    throw new CustomException(ErrorMessages.OrderEmpty, HttpStatusCode.BadRequest);
            //}

            //var orderPrice = _orderRepository.GetOrderPrice(orders);

            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            TokenCardOptions tokenCard = new TokenCardOptions
            {
                Name = model.Name,
                Number = model.Number,
                ExpYear = model.ExpYear,
                ExpMonth = model.ExpMonth,
                Cvc = model.Cvc,
            };

            var tokenOptions = new TokenCreateOptions();

            tokenOptions.Card = tokenCard;

            var tokenService = new TokenService();

            Token token = tokenService.Create(tokenOptions);

            CustomerCreateOptions customer = new CustomerCreateOptions
            {
                Email = model.Email,
                Source = token.Id
            };

            var custSercise = new CustomerService();

            Customer stpCustomer = custSercise.Create(customer);

            var options = new ChargeCreateOptions
            {
                Currency = Curency.USD.ToString(),
                ReceiptEmail = DefaultValues.TestEmailForPay,
                Description = "Test 4 pay with UAH",
                Customer = stpCustomer.Id,
                Amount = 25 * DefaultValues.DefaultAmountValue
            };

            var service = new ChargeService();

            service.Create(options);
        }

        public Task DeleteAsync(PaymentModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<PaymentModel>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<PaymentModel> GetByIdAsync(PaymentModel Modelid)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveAsync(PaymentModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(PaymentModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
