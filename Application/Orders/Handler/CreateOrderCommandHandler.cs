using Application.Orders.Command;
using Application.Orders.Dtos;
using Domain;
using Infrastructure;
using Infrastructure.Repository.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Handler
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly ShippingDbContext _context;

        public CreateOrderCommandHandler(ShippingDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderDesi = request.OrderDesi;

            // 1️⃣ MinDesi - MaxDesi aralığına uyan kargo firmalarını getir
            var carrierConfigs = await _context.CarrierConfigurations
                .Where(c => c.CarrierMinDesi <= orderDesi && c.CarrierMaxDesi >= orderDesi)
                .OrderBy(c => c.CarrierCost) // En düşük fiyatlı olanı seçmek için sıralıyoruz
                .ToListAsync(cancellationToken);

            Carrier selectedCarrier;
            decimal cargoPrice;

            if (carrierConfigs.Any()) // Eğer uygun kargo firmaları bulunduysa
            {
                var selectedConfig = carrierConfigs.First(); // En düşük fiyatlı olanı seçiyoruz
                selectedCarrier = await _context.Carriers.FindAsync(selectedConfig.CarrierId);

                cargoPrice = selectedConfig.CarrierCost;
            }
            else // Eğer desi aralığında uygun bir firma yoksa en yakın olanı bulmalıyız
            {
                var closestConfig = await _context.CarrierConfigurations
                    .OrderBy(c => Math.Abs(c.CarrierMaxDesi - orderDesi)) // En yakın desiyi bul
                    .FirstOrDefaultAsync(cancellationToken);

                if (closestConfig == null)
                {
                    throw new Exception("Uygun kargo firması bulunamadı.");
                }

                selectedCarrier = await _context.Carriers.FindAsync(closestConfig.CarrierId);

                // Fiyat hesaplama:
                int desiFark = orderDesi - closestConfig.CarrierMaxDesi;
                cargoPrice = closestConfig.CarrierCost + (desiFark * selectedCarrier.CarrierPlusDesiCost);
            }

            // 📌 Siparişi oluştur
            var order = new Order
            {
                OrderDesi = orderDesi,
                OrderCarrierCost = cargoPrice,
                CarrierId = selectedCarrier.CarrierId,
                OrderDate = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(cancellationToken);

            
            return order.OrderId;

           
        }
    }
}
