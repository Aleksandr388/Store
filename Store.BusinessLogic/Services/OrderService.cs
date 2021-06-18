using AutoMapper;
using Store.BusinessLogic.Models.Orders;
using Store.BusinessLogic.Models.PaginationModels;
using Store.BusinessLogic.Services.Interfaces;
using Store.DataAcess.Entities;
using Store.DataAcess.Models;
using Store.DataAcess.Repositories.Interfaces;
using System.Collections.Generic;
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
            var mappedModel = _mapper.Map<Order>(orderModel);

            await _orderRepository.CreateAsync(mappedModel);
        }

        public async Task<IEnumerable<OrderModel>> GetAllAsync()
        {
            var models = await _orderRepository.GetAllAsync();

            var result = _mapper.Map<IEnumerable<OrderModel>>(models);

            return result;
        }

        public async Task<ResponseModel<OrderModel>> GetAllOrdersAsync(OrderFiltrationModel model)
        {
            var mappedPageModel = _mapper.Map<OrderFiltration>(model);

            var allModels = await _orderRepository.GetAllOrdersAsync(mappedPageModel);

            var orders = _mapper.Map<IEnumerable<OrderModel>>(allModels);

            var pageInfo = new PageModel(mappedPageModel.PageNumber, mappedPageModel.PageSize);

            var responseModel = new ResponseModel<OrderModel>()
            {
                PageModel = pageInfo,
                NavigationModels = orders
            };

            return responseModel;
        }
    }
}
