using Microsoft.AspNetCore.Mvc;
using Wms.Infrastructure.Models;
using Wms.Infrastructure.Services;
using System.Threading.Tasks;
using OpenTelemetry.Trace;

namespace Wms.Api.Controllers.Areas.@operator
{
    [Area("operator")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/v1/order
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrders();
            return Ok(result);
        }

        // GET: api/v1/order/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var result = await _orderService.GetOrderById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/v1/order
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateOrder([FromBody] OrderDto orderDto)
        {
            var order = new Order()
            {
                Id = orderDto.Id,
                Status = orderDto.Status,
                Type = orderDto.Type,
                SourceId = orderDto.SourceId,
                SampleId = orderDto.SampleId,
                TargetId = orderDto.TargetId
            };

            await _orderService.CreateOrUpdateOrder(order);
            return Ok();
        }

        // DELETE: api/v1/order/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrder(id);
            return Ok();
        }
    }
}