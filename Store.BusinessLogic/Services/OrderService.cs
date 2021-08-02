using AutoMapper;
using Shared.Constants;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.PaginationModels;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAcess.Entities;
using Store.DataAcess.Models;
using Store.DataAcess.Repositories.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Store.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(OrderModel orderModel)
        {
            if (orderModel is null)
            {
                throw new CustomException(ErrorMessages.OrderEmpty, HttpStatusCode.BadRequest);
            }

            var mappedModel = _mapper.Map<Order>(orderModel);

            await _orderRepository.CreateAsync(mappedModel);
        }

        public async Task<ResponseModel<OrderModel>> GetAllOrdersAsync(OrderFiltrationModel model)
        {
            if (model is null)
            {
                throw new CustomException(ErrorMessages.OrderEmpty, HttpStatusCode.BadRequest);
            }

            var mappedPageModel = _mapper.Map<OrderFiltration>(model);

            (IEnumerable<Order> orders, int count) orderCount = await _orderRepository.GetAllOrdersAsync(mappedPageModel);

            var orders = _mapper.Map<IEnumerable<OrderModel>>(orderCount.orders);

            var pageInfo = new PageModel(mappedPageModel.PageNumber, mappedPageModel.PageSize, orderCount.count);

            var responseModel = new ResponseModel<OrderModel>()
            {
                PageModel = pageInfo,
                NavigationModels = orders
            };

            return responseModel;
        }
    }
}
