using Microsoft.AspNetCore.Mvc;
using Wms.Infrastructure.Models;
using Wms.Infrastructure.Services;
using System.Threading.Tasks;

namespace Wms.Api.Controllers.Areas.@operator
{
    [Area("operator")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ISampleService _sampleService;

        public SampleController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        // GET: api/v1/sample
        [HttpGet]
        public async Task<IActionResult> GetAllSamples()
        {
            var result = await _sampleService.GetAllSamples();
            return Ok(result);
        }

        // GET: api/v1/sample/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSampleById(int id)
        {
            var result = await _sampleService.GetSampleById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/v1/sample
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateSample([FromBody] SampleDto sampleDto)
        {
            var sample = new Sample()
            {
                Id = sampleDto.Id,
                Barcode = sampleDto.Barcode,
                ParentId = sampleDto.ParentId
            };

            await _sampleService.CreateOrUpdateSample(sample);
            return Ok();
        }

        // DELETE: api/v1/sample/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSample(int id)
        {
            await _sampleService.DeleteSample(id);
            return Ok();
        }
    }
}