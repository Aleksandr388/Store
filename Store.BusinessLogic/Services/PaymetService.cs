using AutoMapper;
using Microsoft.Extensions.Options;
using Shared.Constants;
using Shared.Enums;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.Payments;
using Store.BusinessLogic.Options;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAcess.Entities;
using Store.DataAcess.Repositories.Interfaces;
using Stripe;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class PaymetService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly StripeOptions _stripeOptions;
        private readonly IPrintingEditionRepository _printingEditionRepository;

        public PaymetService(IMapper mapper, IOptions<StripeOptions> options, IPaymentRepository paymentRepository, IOrderRepository orderRepository, IPrintingEditionRepository printingEditionRepository)
        {
            _printingEditionRepository = printingEditionRepository;
            _stripeOptions = options.Value;
            _mapper = mapper;
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
        }

        public async Task CreatePaymentAsync(PayModel model, long userId)
        {
            OrderModel order = new OrderModel()
            {
                UserId = userId,
                Description = model.Description,
            };

            var mappedOrder = _mapper.Map<DataAcess.Entities.Order>(order);

            await _orderRepository.CreateAsync(mappedOrder);

            if (model.OrderItems is null)
            {
                throw new CustomException(ErrorMessages.OrderItemIsEmpty, HttpStatusCode.BadRequest);
            }

            var newOrderItems = _mapper.Map<List<DataAcess.Entities.OrderItem>>(model.OrderItems);

            var editionPrices = await _printingEditionRepository.GetEditionsPrices(newOrderItems);

            foreach (var item in newOrderItems)
            {
                item.Price = editionPrices.FirstOrDefault(x => x.Id == item.PrintingEditionId).Price;
            }

            mappedOrder.OrderItems.AddRange(newOrderItems);

            await _orderRepository.SaveChagesAsync();

            var orderPrice = _orderRepository.GetOrderPrice(mappedOrder);

            if (orderPrice is default(int))
            {
                throw new CustomException(ErrorMessages.PriceIsEmpty, HttpStatusCode.BadRequest);
            }

            StripeConfiguration.ApiKey = _stripeOptions.SecretKey;

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
                Currency = CurencyType.USD.ToString(),
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

            mappedOrder.OrderStatus = OrderStatus.Paid;
            mappedOrder.PaymentId = paymentId.Id;

            await _orderRepository.UpdateAsync(mappedOrder);
        }
    }
}
