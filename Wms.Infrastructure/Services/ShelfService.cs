using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wms.Infrastructure.Models;
using Wms.Infrastructure.Models.Other;

namespace Wms.Infrastructure.Services
{
    public interface IShelfService
    {
        Task<List<Shelf>> GetAllShelves();
        Task<Shelf?> GetShelfById(int id);
        Task CreateOrUpdateShelf(Shelf shelf);
        Task<DeleteResult> DeleteShelf(int id);
    }

    public class ShelfService : IShelfService
    {
        private readonly WmsDbContext _dbContext;

        public ShelfService(WmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Shelf>> GetAllShelves()
        {
            return await _dbContext.Shelves
                .Include(s => s.Area)
                    .ThenInclude(a => a.Warehouse)
                .Include(s => s.StorageLocations)
                .ToListAsync();
        }

        public async Task<Shelf?> GetShelfById(int id)
        {
            return await _dbContext.Shelves
                .Include(s => s.Area)
                    .ThenInclude(a => a.Warehouse)
                .Include(s => s.StorageLocations)
                .FirstOrDefaultAsync(s => s.Id == id);
        }


        public async Task CreateOrUpdateShelf(Shelf shelf)
        {
            var area = await _dbContext.Areas.FindAsync(shelf.AreaId);
            if (area == null)
            {
                throw new ArgumentException("The provided AreaId does not correspond to an existing Area.");
            }

            var existingShelf = await _dbContext.Shelves.FindAsync(shelf.Id);
            if (existingShelf == null)
            {
                shelf.Area = area; // set the area
                await _dbContext.Shelves.AddAsync(shelf);
            }
            else
            {
                existingShelf.Area = area; // update the area
                _dbContext.Entry(existingShelf).CurrentValues.SetValues(shelf);
            }

            await _dbContext.SaveChangesAsync();
        }


        public async Task<DeleteResult> DeleteShelf(int id)
        {
            var shelf = await _dbContext.Shelves.Include(s => s.StorageLocations).FirstOrDefaultAsync(s => s.Id == id);

            if (shelf == null)
                return new DeleteResult { Success = false, Message = "Shelf not found." };

            if (shelf.StorageLocations.Any())
                return new DeleteResult { Success = false, Message = "Cannot delete shelf as it contains storage locations. Please delete the storage locations first." };

            _dbContext.Shelves.Remove(shelf);
            await _dbContext.SaveChangesAsync();

            return new DeleteResult { Success = true };
        }

    }

}
