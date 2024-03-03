using Microsoft.EntityFrameworkCore;
using Wms.Infrastructure.Models;

namespace Wms.Infrastructure.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task CreateOrUpdateOrder(Order order);
        Task DeleteOrder(int id);
    }

    public class OrderService : IOrderService
    {
        private readonly WmsDbContext _dbContext;

        public OrderService(WmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _dbContext.Orders.FindAsync(id);
        }

        public async Task CreateOrUpdateOrder(Order order)
        {
            if (_dbContext.Orders.Any(o => o.Id == order.Id))
            {
                _dbContext.Entry(order).State = EntityState.Modified;
            }
            else
            {
                await _dbContext.Orders.AddAsync(order);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrder(int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order != null)
            {
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}
