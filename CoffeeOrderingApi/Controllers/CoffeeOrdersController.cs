using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using CoffeeOrderingApiWithCQRSandMediatR.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeOrderingApiWithCQRSandMediatR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoffeeOrdersController : ControllerBase
    {
        private readonly ICoffeeOrderService _orderService;

        public CoffeeOrdersController(ICoffeeOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoffeeOrderResponseDto>>> GetAllOrders()
        {
            var result = await _orderService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CoffeeOrderResponseDto>> GetOrderById(int id)
        {
            var result = await _orderService.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CoffeeOrderResponseDto>> CreateOrder([FromBody] CreateCoffeeOrderDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _orderService.CreateAsync(dto);
                if (result == null)
                {
                    return BadRequest("Unable to create order.");
                }
                return CreatedAtAction(nameof(GetOrderById), new { id = result.Id }, result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteAsync(id);
            if (!result)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            return NoContent();
        }
    }
}