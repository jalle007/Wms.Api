using Microsoft.AspNetCore.Mvc;
using Wms.Infrastructure.Models;
using Wms.Infrastructure.Services;
using System.Threading.Tasks;
using Steeltoe.Common.Order;

namespace Wms.Api.Controllers.Areas.@operator
{
    [Area("operator")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class OrderTraceController : ControllerBase
    {
        private readonly IOrderTraceService _orderTraceService;

        public OrderTraceController(IOrderTraceService orderTraceService)
        {
            _orderTraceService = orderTraceService;
        }

        // GET: api/v1/order-trace
        [HttpGet]
        public async Task<IActionResult> GetAllOrderTraces()
        {
            var result = await _orderTraceService.GetAllOrderTraces();
            return Ok(result);
        }

        // GET: api/v1/order-trace/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderTraceById(int id)
        {
            var result = await _orderTraceService.GetOrderTraceById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/v1/order-trace
        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateOrderTrace([FromBody] OrderTraceDto orderTraceDto)
        {
            var orderTrace = new OrderTrace()
            {
                Id = orderTraceDto.Id,
                OrderId = orderTraceDto.OrderId,
                UserId = orderTraceDto.UserId
            };

            await _orderTraceService.CreateOrUpdateOrderTrace(orderTrace);
            return Ok();
        }

        // DELETE: api/v1/order-trace/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderTrace(int id)
        {
            await _orderTraceService.DeleteOrderTrace(id);
            return Ok();
        }
    }
}