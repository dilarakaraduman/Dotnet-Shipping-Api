using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class CarrierReportService
    {
        private readonly ShippingDbContext _context;

        public CarrierReportService(ShippingDbContext context)
        {
            _context = context;
        }

        public async Task GenerateCarrierReports()
        {
            var reports = await _context.Orders
                .GroupBy(o => new { o.CarrierId, o.OrderDate.Date })
                .Select(group => new CarrierReport
                {
                    CarrierId = group.Key.CarrierId,
                    CarrierReportDate = group.Key.Date,
                    CarrierCost = group.Sum(o => o.OrderCarrierCost)
                })
                .ToListAsync();

            _context.CarrierReports.AddRange(reports);
            await _context.SaveChangesAsync();
        }
    }
}
