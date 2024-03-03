using Microsoft.AspNetCore.Mvc;
using Wms.Infrastructure.Models;
using Wms.Infrastructure.Services;
using System.Threading.Tasks;

namespace Wms.Api.Controllers.Areas.admin
{
    [Area("admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        // GET: api/v1/location
        [HttpGet]
        public async Task<IActionResult> GetAllLocations()
        {
            var result = await _locationService.GetAllLocations();
            return Ok(result);
        }

        // GET: api/v1/location/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationById(int id)
        {
            var result = await _locationService.GetLocationById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/v1/location
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateLocation([FromBody] LocationDto locationDto)
        {

            var location = new Location()
            {
                Id = locationDto.Id,
                LocationName = locationDto.LocationName,
                Address = locationDto.Address,
                City = locationDto.City,
                Country = locationDto.Country
            };

            await _locationService.CreateOrUpdateLocation(location);
            return Ok();
        }

        // DELETE: api/v1/location/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var result = await _locationService.DeleteLocation(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok();
        }

    }
}