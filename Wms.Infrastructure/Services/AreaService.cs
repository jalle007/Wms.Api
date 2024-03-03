using Microsoft.EntityFrameworkCore;
using UoN.ExpressiveAnnotations.NetCore.Analysis;
using Wms.Infrastructure.Models;
using Wms.Infrastructure.Models.Other;

namespace Wms.Infrastructure.Services
{
    public interface IAreaService
    {
        Task<List<Area>> GetAllAreas();
        Task<Area?> GetAreaById(int id);
        Task CreateOrUpdateArea(Area area);
        Task<DeleteResult> DeleteArea(int id);
    }

    public class AreaService : IAreaService
    {
        private readonly WmsDbContext _dbContext;

        public AreaService(WmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Area>> GetAllAreas()
        {
            return await _dbContext.Areas
                .Include(a => a.Warehouse)
                    .ThenInclude(w => w.Location)
                .Include(a => a.Shelves)
                .ToListAsync();
        }

        public async Task<Area?> GetAreaById(int id)
        {
            return await _dbContext.Areas
                .Include(a => a.Warehouse)
                    .ThenInclude(w => w.Location)
                .Include(a => a.Shelves)
                .SingleOrDefaultAsync(a => a.Id == id);
        }


        public async Task CreateOrUpdateArea(Area area)
        {
            // Load the Warehouse entity
            var warehouse = await _dbContext.Warehouses.FindAsync(area.WarehouseId);
            if (warehouse == null)
            {
                throw new ArgumentException("The provided WarehouseId does not correspond to an existing Warehouse.");
            }

            var existingArea = await _dbContext.Areas.FindAsync(area.Id);
            if (existingArea == null)
            {
                area.Warehouse = warehouse; // set the warehouse
                await _dbContext.Areas.AddAsync(area);
            }
            else
            {
                existingArea.Warehouse = warehouse; // update the warehouse
                _dbContext.Entry(existingArea).CurrentValues.SetValues(area);
            }
 
            await _dbContext.SaveChangesAsync();
        }

        public async Task<DeleteResult> DeleteArea(int id)
        {
            var area = await _dbContext.Areas.Include(a => a.Shelves).FirstOrDefaultAsync(a => a.Id == id);

            if (area == null)
                return new DeleteResult { Success = false, Message = "Area not found." };

            if (area.Shelves.Any())
                return new DeleteResult { Success = false, Message = "Cannot delete area as it contains shelves. Please delete the shelves first." };

            _dbContext.Areas.Remove(area);
            await _dbContext.SaveChangesAsync();

            return new DeleteResult { Success = true };
        }

    }

}
