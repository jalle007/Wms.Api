using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wms.Infrastructure.Models;

namespace Wms.Infrastructure.Services
{
    public interface IOrderTraceService
    {
        Task<List<OrderTrace>> GetAllOrderTraces();
        Task<OrderTrace> GetOrderTraceById(int id);
        Task CreateOrUpdateOrderTrace(OrderTrace orderTrace);
        Task DeleteOrderTrace(int id);
    }

    public class OrderTraceService : IOrderTraceService
    {
        private readonly WmsDbContext _dbContext;

        public OrderTraceService(WmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OrderTrace>> GetAllOrderTraces()
        {
            return await _dbContext.OrderTraces.ToListAsync();
        }

        public async Task<OrderTrace> GetOrderTraceById(int id)
        {
            return await _dbContext.OrderTraces.FindAsync(id);
        }

        public async Task CreateOrUpdateOrderTrace(OrderTrace orderTrace)
        {
            if (_dbContext.OrderTraces.Find(orderTrace.Id) == null)
            {
                await _dbContext.OrderTraces.AddAsync(orderTrace);
            }
            else
            {
                _dbContext.Entry(orderTrace).State = EntityState.Modified;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderTrace(int id)
        {
            var orderTrace = await _dbContext.OrderTraces.FindAsync(id);
            if (orderTrace != null)
            {
                _dbContext.OrderTraces.Remove(orderTrace);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}
