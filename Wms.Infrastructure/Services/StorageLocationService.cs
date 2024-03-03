using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wms.Infrastructure.Models;

namespace Wms.Infrastructure.Services
{
    public interface IStorageLocationService
    {
        Task<List<StorageLocation>> GetAllStorageLocations();
        Task<StorageLocation?> GetStorageLocationById(int id);
        Task CreateOrUpdateStorageLocation(StorageLocation storageLocation);
        Task DeleteStorageLocation(int id);
    }

    public class StorageLocationService : IStorageLocationService
    {
        private readonly WmsDbContext _dbContext;

        public StorageLocationService(WmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<StorageLocation>> GetAllStorageLocations()
        {
            return await _dbContext.StorageLocations
                .Include(sl => sl.Shelf)
                    .ThenInclude(s => s.Area)
                        .ThenInclude(a => a.Warehouse)
                            .ThenInclude(w => w.Location)
                .ToListAsync();
        }

        public async Task<StorageLocation?> GetStorageLocationById(int id)
        {
            return await _dbContext.StorageLocations
                .Include(sl => sl.Shelf)
                    .ThenInclude(s => s.Area)
                        .ThenInclude(a => a.Warehouse)
                            .ThenInclude(w => w.Location)
                .FirstOrDefaultAsync(sl => sl.Id == id);
        }



        public async Task CreateOrUpdateStorageLocation(StorageLocation storageLocation)
        {
            var location = await _dbContext.Locations.FindAsync(storageLocation.LocationId);
            if (await _dbContext.Locations.FindAsync(storageLocation.LocationId) == null)
            {
                throw new ArgumentException("The provided LocationId does not correspond to an existing Location.");
            }

            var warehouse = await _dbContext.Warehouses.FindAsync(storageLocation.WarehouseId);
            if (warehouse == null)
            {
                throw new ArgumentException("The provided WarehouseId does not correspond to an existing Warehouse.");
            }

            var area = await _dbContext.Areas.FindAsync(storageLocation.AreaId);
            if (area == null)
            {
                throw new ArgumentException("The provided AreaId does not correspond to an existing Area.");
            }

            var shelf = await _dbContext.Shelves.FindAsync(storageLocation.ShelfId);
            if (await _dbContext.Shelves.FindAsync(storageLocation.ShelfId) == null)
            {
                throw new ArgumentException("The provided ShelfId does not correspond to an existing Shelf.");
            }

          
            var existingStorageLocation = await _dbContext.StorageLocations.FindAsync(storageLocation.Id);
            if (existingStorageLocation == null)
            {
                storageLocation.Location = location;
                storageLocation.Warehouse= warehouse;
                storageLocation.Area= area; 
                storageLocation.Shelf = shelf; 
                await _dbContext.StorageLocations.AddAsync(storageLocation);
            }
            else
            {
                existingStorageLocation.Location = location;
                existingStorageLocation.Warehouse = warehouse;
                existingStorageLocation.Area = area;
                existingStorageLocation.Shelf = shelf;
                _dbContext.Entry(existingStorageLocation).CurrentValues.SetValues(storageLocation);
            }

            await _dbContext.SaveChangesAsync();
        }


        public async Task DeleteStorageLocation(int id)
        {
            var storageLocation = await _dbContext.StorageLocations.FindAsync(id);
            if (storageLocation != null)
            {
                _dbContext.StorageLocations.Remove(storageLocation);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

}
