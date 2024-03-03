using Microsoft.AspNetCore.Mvc;
using Wms.Infrastructure.Models;
using Wms.Infrastructure.Services;
using System.Threading.Tasks;
using System;

namespace Wms.Api.Controllers.Areas.admin
{
    [Area("admin")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class ShelfController : ControllerBase
    {
        private readonly IShelfService _shelfService;

        public ShelfController(IShelfService shelfService)
        {
            _shelfService = shelfService;
        }

        // GET: api/v1/shelf
        [HttpGet]
        public async Task<IActionResult> GetAllShelves()
        {
            var result = await _shelfService.GetAllShelves();
            return Ok(result);
        }

        // GET: api/v1/shelf/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShelfById(int id)
        {
            var result = await _shelfService.GetShelfById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/v1/shelf
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateShelf([FromBody] ShelfDto shelfDto)
        {
            var shelf = new Shelf()
            {
                Id = shelfDto.Id,
                ShelfName = shelfDto.ShelfName,
                AreaId = shelfDto.AreaId
            };

            await _shelfService.CreateOrUpdateShelf(shelf);
            return Ok();
        }

        // DELETE: api/v1/shelf/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShelf(int id)
        {
            var result = await _shelfService.DeleteShelf(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok();
        }

    }
}