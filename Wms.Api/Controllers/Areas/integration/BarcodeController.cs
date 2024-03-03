using Microsoft.AspNetCore.Mvc;
using Wms.Infrastructure.Services.Other;

namespace Wms.Api.Controllers.Areas.integration
{
    [Area("integration")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class BarcodeController : ControllerBase
    {
        private readonly IBarcodeService _barcodeService;

        public BarcodeController(IBarcodeService barcodeService)
        {
            _barcodeService = barcodeService;
        }

        /// <summary>
        /// - implemented, not tested.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateBarcode([FromBody] string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return BadRequest("Text cannot be null or empty.");
            }

            var response = await _barcodeService.CreateBarcode(text);
            return Ok(response);
        }
    }
}
