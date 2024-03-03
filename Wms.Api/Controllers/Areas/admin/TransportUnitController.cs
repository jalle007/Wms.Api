using Microsoft.AspNetCore.Mvc;
using Wms.Infrastructure.Models;
using Wms.Infrastructure.Services;
using System.Threading.Tasks;

namespace Wms.Api.Controllers.Areas.admin
{
    [Area("admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class TransportUnitController : ControllerBase
    {
        private readonly ITransportUnitService _transportUnitService;

        public TransportUnitController(ITransportUnitService transportUnitService)
        {
            _transportUnitService = transportUnitService;
        }

        // GET: api/v1/transport-unit
        [HttpGet]
        public async Task<IActionResult> GetAllTransportUnits()
        {
            var result = await _transportUnitService.GetAllTransportUnits();
            return Ok(result);
        }

        // GET: api/v1/transport-unit/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransportUnitById(int id)
        {
            var result = await _transportUnitService.GetTransportUnitById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/v1/transport-unit
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateTransportUnit([FromBody] TransportUnitDto transportUnitDto)
        {
            var transportUnit = new TransportUnit()
            {
                Id = transportUnitDto.Id,
                TransportUnitName = transportUnitDto.TransportUnitName
            };

            await _transportUnitService.CreateOrUpdateTransportUnit(transportUnit);
            return Ok();
        }

        // DELETE: api/v1/transport-unit/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransportUnit(int id)
        {
            await _transportUnitService.DeleteTransportUnit(id);
            return Ok();
        }
    }
}
