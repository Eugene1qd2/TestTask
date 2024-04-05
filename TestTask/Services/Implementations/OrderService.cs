using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        ApplicationDbContext _context;
        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrder()
        {
            var result = await _context.Orders
                .OrderByDescending(od=>od.Price*od.Quantity)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<Order>> GetOrders()
        {
            var result = await _context.Orders
                .Where(od=>od.Quantity>10)
                .ToListAsync();

            return result;
        }
    }
}
