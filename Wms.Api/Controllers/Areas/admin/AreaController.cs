using Microsoft.AspNetCore.Mvc;
using Wms.Infrastructure.Models;
using Wms.Infrastructure.Services;
using System.Threading.Tasks;

namespace Wms.Api.Controllers.Areas.admin
{
    [Area("admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly IAreaService _areaService;

        public AreaController(IAreaService areaService)
        {
            _areaService = areaService;
        }

        // GET: api/v1/area
        [HttpGet]
        public async Task<IActionResult> GetAllAreas()
        {
            var result = await _areaService.GetAllAreas();
            return Ok(result);
        }

        // GET: api/v1/area/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAreaById(int id)
        {
            var result = await _areaService.GetAreaById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/v1/area
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateArea([FromBody] AreaDto areaDto)
        {

            var area = new Area()
            {
                Id = areaDto.Id,
                AreaName = areaDto.AreaName,
                WarehouseId = areaDto.WarehouseId
            };


            await _areaService.CreateOrUpdateArea(area);
            return Ok();
        }

        // DELETE: api/v1/area/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArea(int id)
        {
            var result = await _areaService.DeleteArea(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok();
        }

    }
}
