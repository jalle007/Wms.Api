using Microsoft.AspNetCore.Mvc;
using Wms.Infrastructure.Models;
using Wms.Infrastructure.Services;
using System.Threading.Tasks;

namespace Wms.Api.Controllers.Areas.admin
{
    [Area("admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class StorageLocationController : ControllerBase
    {
        private readonly IStorageLocationService _storageLocationService;

        public StorageLocationController(IStorageLocationService storageLocationService)
        {
            _storageLocationService = storageLocationService;
        }

        // GET: api/v1/storage-location
        [HttpGet]
        public async Task<IActionResult> GetAllStorageLocations()
        {
            var result = await _storageLocationService.GetAllStorageLocations();
            return Ok(result);
        }

        // GET: api/v1/storage-location/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStorageLocationById(int id)
        {
            var result = await _storageLocationService.GetStorageLocationById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/v1/storage-location
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateStorageLocation([FromBody] StorageLocationDto storageLocationDto)
        {
            var storageLocation = new StorageLocation()
            {
                Id = storageLocationDto.Id,
                LocationId = storageLocationDto.LocationId,
                WarehouseId = storageLocationDto.WarehouseId,
                AreaId = storageLocationDto.AreaId,
                ShelfId = storageLocationDto.ShelfId,
                Row = storageLocationDto.Row,
                Column = storageLocationDto.Column
            };

            await _storageLocationService.CreateOrUpdateStorageLocation(storageLocation);
            return Ok();
        }

        // DELETE: api/v1/storage-location/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStorageLocation(int id)
        {
            await _storageLocationService.DeleteStorageLocation(id);
            return Ok();
        }
    }
}