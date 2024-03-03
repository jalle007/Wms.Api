using Microsoft.AspNetCore.Mvc;
using Wms.Infrastructure.Models;
using Wms.Infrastructure.Services;
using System.Threading.Tasks;

namespace Wms.Api.Controllers.Areas.admin
{
    [Area("admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        // GET: api/v1/warehouse
        [HttpGet]
        public async Task<IActionResult> GetAllWarehouses()
        {
            var result = await _warehouseService.GetAllWarehouses();
            return Ok(result);
        }

        // GET: api/v1/warehouse/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouseById(int id)
        {
            var result = await _warehouseService.GetWarehouseById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/v1/warehouse
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateWarehouse([FromBody] WarehouseDto warehouseDto)
        {
            var warehouse = new Warehouse
            {
                Id = warehouseDto.Id,
                WarehouseName = warehouseDto.WarehouseName,
                LocationId = warehouseDto.LocationId
            };

            await _warehouseService.CreateOrUpdateWarehouse(warehouse);
            return Ok();
        }


        // DELETE: api/v1/warehouse/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            var result = await _warehouseService.DeleteWarehouse(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok();
        }

    }
}
