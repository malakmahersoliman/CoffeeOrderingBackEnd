using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using CoffeeOrderingApiWithCQRSandMediatR.Features.CoffeeOrders.Commands.CreateCoffeeOrder;
using CoffeeOrderingApiWithCQRSandMediatR.Features.CoffeeOrders.Commands.DeleteCoffeeOrder;
using CoffeeOrderingApiWithCQRSandMediatR.Features.CoffeeOrders.Queries.GetAllCoffeeOrders;
using CoffeeOrderingApiWithCQRSandMediatR.Features.CoffeeOrders.Queries.GetCoffeeOrderById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeOrderingApiWithCQRSandMediatR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoffeeOrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CoffeeOrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoffeeOrderResponseDto>>> GetAllOrders()
        {
            var result = await _mediator.Send(new GetAllCoffeeOrdersQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CoffeeOrderResponseDto>> GetOrderById(int id)
        {
            var result = await _mediator.Send(new GetCoffeeOrderByIdQuery(id));
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
                var command = new CreateCoffeeOrderCommand
                {
                    CustomerId = dto.CustomerId,
                    IsTakeAway = dto.IsTakeAway,
                    Items = dto.Items
                };

                var result = await _mediator.Send(command);
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
            var result = await _mediator.Send(new DeleteCoffeeOrderCommand(id));
            if (!result)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            return NoContent();
        }
    }
}