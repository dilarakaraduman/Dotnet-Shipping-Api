using Domain;
using Infrastructure.Repository.Abstract;

namespace Application.Concrete
{
    public class OrderService
    {
        private readonly IRepository<Domain.Order> _orderRepository;
        private readonly IRepository<Carrier> _carrierRepository;
        private readonly IRepository<CarrierConfiguration> _carrierConfigRepository;

        public OrderService(IRepository<Domain.Order> orderRepository, IRepository<Carrier> carrierRepository, IRepository<CarrierConfiguration> carrierConfigRepository)
        {
            _orderRepository = orderRepository;
            _carrierRepository = carrierRepository;
            _carrierConfigRepository = carrierConfigRepository;
        }

        public async Task<IEnumerable<Domain.Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Domain.Order?> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<int> AddOrderAsync(int orderDesi)
        {
            var carrierConfigs = await _carrierConfigRepository.GetAllAsync();
            var suitableCarrierConfig = carrierConfigs
                .Where(c => orderDesi >= c.CarrierMinDesi && orderDesi <= c.CarrierMaxDesi)
                .OrderBy(c => c.CarrierCost)
                .FirstOrDefault();

            decimal shippingCost;
            int carrierId;

            if (suitableCarrierConfig != null)
            {
                shippingCost = suitableCarrierConfig.CarrierCost;
                carrierId = suitableCarrierConfig.CarrierId;
            }
            else
            {
                var nearestCarrierConfig = carrierConfigs
                    .OrderBy(c => Math.Abs(orderDesi - c.CarrierMinDesi))
                    .FirstOrDefault();

                if (nearestCarrierConfig == null)
                    throw new Exception("Uygun kargo firması bulunamadı.");

                int excessDesi = orderDesi - nearestCarrierConfig.CarrierMaxDesi;
                shippingCost = nearestCarrierConfig.CarrierCost + (excessDesi * nearestCarrierConfig.Carrier.CarrierPlusDesiCost);
                carrierId = nearestCarrierConfig.CarrierId;
            }

            var newOrder = new Domain.Order
            {
                OrderDesi = orderDesi,
                OrderDate = DateTime.UtcNow,
                OrderCarrierCost = shippingCost,
                CarrierId = carrierId
            };

            await _orderRepository.AddAsync(newOrder);
            return newOrder.OrderId;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            await _orderRepository.DeleteAsync(id);
            return true;
        }
    }
}

