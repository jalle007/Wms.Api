using Microsoft.EntityFrameworkCore;
using Wms.Infrastructure.Models;
using Wms.Infrastructure.Models.Other;

namespace Wms.Infrastructure.Services
{
    public interface ILocationService
    {
        Task<List<Location>> GetAllLocations();
        Task<Location?> GetLocationById(int id);
        Task CreateOrUpdateLocation(Location location);
        Task<DeleteResult> DeleteLocation(int id);
    }

    public class LocationService : ILocationService
    {
        private readonly WmsDbContext _dbContext;

        public LocationService(WmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Location>> GetAllLocations()
        {
            return await _dbContext.Locations
                .Include(l => l.VmsConfigs)
                .Include(l => l.Warehouses)
                    .ThenInclude(w => w.Areas)
                        .ThenInclude(a => a.Shelves)
                .ToListAsync();
        }

        public async Task<Location?> GetLocationById(int id)
        {
            return await _dbContext.Locations
                .Include(l => l.VmsConfigs)
                .Include(l => l.Warehouses)
                    .ThenInclude(w => w.Areas)
                        .ThenInclude(a => a.Shelves)
                .FirstOrDefaultAsync(l => l.Id == id);
        }


        public async Task CreateOrUpdateLocation(Location location)
        {
            if (_dbContext.Locations.Any(l => l.Id == location.Id))
            {
                _dbContext.Entry(location).State = EntityState.Modified;
            }
            else
            {
                await _dbContext.Locations.AddAsync(location);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<DeleteResult> DeleteLocation(int id)
        {
            var location = await _dbContext.Locations.Include(l => l.Warehouses).FirstOrDefaultAsync(l => l.Id == id);

            if (location == null)
                return new DeleteResult { Success = false, Message = "Location not found." };

            if (location.Warehouses.Any())
                return new DeleteResult { Success = false, Message = "Cannot delete location as it contains warehouses. Please delete the warehouses first." };

            _dbContext.Locations.Remove(location);
            await _dbContext.SaveChangesAsync();

            return new DeleteResult { Success = true };
        }

    }
}
