using Microsoft.EntityFrameworkCore;
using Wms.Infrastructure.Models;
using System.Linq;

namespace Wms.Infrastructure.Services
{
    public interface ITransportUnitService
    {
        Task<List<TransportUnit>> GetAllTransportUnits();
        Task<TransportUnit?> GetTransportUnitById(int id);
        Task CreateOrUpdateTransportUnit(TransportUnit transportUnit);
        Task DeleteTransportUnit(int id);
    }

    public class TransportUnitService : ITransportUnitService
    {
        private readonly WmsDbContext _dbContext;

        public TransportUnitService(WmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TransportUnit>> GetAllTransportUnits()
        {
            return await _dbContext.TransportUnits.ToListAsync();
        }

        public async Task<TransportUnit?> GetTransportUnitById(int id)
        {
            return await _dbContext.TransportUnits.FindAsync(id);
        }

        public async Task CreateOrUpdateTransportUnit(TransportUnit transportUnit)
        {
          

            if (_dbContext.TransportUnits.Any(t => t.Id == transportUnit.Id))
            {
                _dbContext.Entry(transportUnit).State = EntityState.Modified;
            }
            else
            {
                await _dbContext.TransportUnits.AddAsync(transportUnit);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTransportUnit(int id)
        {
            var transportUnit = await _dbContext.TransportUnits.FindAsync(id);
            if (transportUnit != null)
            {
                _dbContext.TransportUnits.Remove(transportUnit);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
