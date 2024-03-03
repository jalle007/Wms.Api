using Microsoft.EntityFrameworkCore;
using Wms.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Wms.Infrastructure.Models.Other;

namespace Wms.Infrastructure.Services
{
    public interface IWarehouseService
    {
        Task<List<Warehouse>> GetAllWarehouses();
        Task<Warehouse?> GetWarehouseById(int id);
        Task CreateOrUpdateWarehouse(Warehouse warehouse);
        Task<DeleteResult> DeleteWarehouse(int id);
    }

    public class WarehouseService : IWarehouseService
    {
        private readonly WmsDbContext _dbContext;

        public WarehouseService(WmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Warehouse>> GetAllWarehouses()
        {
            return await _dbContext.Warehouses
                .Include(w => w.Location)
                .Include(w => w.Areas)
                    .ThenInclude(a => a.Shelves)
                .Include(w => w.StorageLocations)
                .ToListAsync();
        }

        public async Task<Warehouse?> GetWarehouseById(int id)
        {
            return await _dbContext.Warehouses
                .Include(w => w.Location)
                .Include(w => w.Areas)
                    .ThenInclude(a => a.Shelves)
                .Include(w => w.StorageLocations)
                .SingleOrDefaultAsync(w => w.Id == id);
        }



        public async Task CreateOrUpdateWarehouse(Warehouse warehouse)
        {
            // Load the Location entity
            var location = await _dbContext.Locations.FindAsync(warehouse.LocationId);
            if (location == null)
            {
                throw new ArgumentException("The provided LocationId does not correspond to an existing Location.");
            }

            var existingWarehouse = await _dbContext.Warehouses.FindAsync(warehouse.Id);
            if (existingWarehouse == null)
            {
                warehouse.Location = location; // set the Location
                await _dbContext.Warehouses.AddAsync(warehouse);
            }
            else
            {
                existingWarehouse.Location = location; // update the Location
                _dbContext.Entry(existingWarehouse).CurrentValues.SetValues(warehouse);
            }

            //_dbContext.Configuration.ValidateOnSaveEnabled = false;
            await _dbContext.SaveChangesAsync();
        }


        public async Task<DeleteResult> DeleteWarehouse(int id)
        {
            var warehouse = await _dbContext.Warehouses.Include(w => w.Areas).FirstOrDefaultAsync(w => w.Id == id);

            if (warehouse == null)
                return new DeleteResult { Success = false, Message = "Warehouse not found." };

            if (warehouse.Areas.Any())
                return new DeleteResult { Success = false, Message = "Cannot delete warehouse as it contains areas. Please delete the areas first." };

            _dbContext.Warehouses.Remove(warehouse);
            await _dbContext.SaveChangesAsync();

            return new DeleteResult { Success = true };
        }

    }
}
