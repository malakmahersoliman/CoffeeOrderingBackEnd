using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using CoffeeOrderingApiWithCQRSandMediatR.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeOrderingApiWithCQRSandMediatR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoffeeOrdersController : ControllerBase
    {
        private readonly ICoffeeOrderService _coffeeOrderService;

        public CoffeeOrdersController(ICoffeeOrderService coffeeOrderService)
        {
            _coffeeOrderService = coffeeOrderService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CoffeeOrderResponseDto>>> GetAll()
        {
            var orders = await _coffeeOrderService.GetAll();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CoffeeOrderResponseDto>> GetById(int id)
        {
            var order = await _coffeeOrderService.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<CoffeeOrderResponseDto>> Create(CreateCoffeeOrderDto dto)
        {
            var createdOrder = await _coffeeOrderService.Create(dto);

            if (createdOrder == null)
            {
                return BadRequest("Invalid customer or menu item.");
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdOrder.Id },
                createdOrder
            );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _coffeeOrderService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}