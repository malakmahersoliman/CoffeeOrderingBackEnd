using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Commands.CreateMenuItem;
using CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Commands.DeleteMenuItem;
using CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Commands.UpdateMenuItem;
using CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Queries.GetAllMenuItems;
using CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Queries.GetMenuItemById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeOrderingApiWithCQRSandMediatR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MenuItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemResponseDto>>> GetAllMenuItems()
        {
            var result = await _mediator.Send(new GetAllMenuItemsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemResponseDto>> GetMenuItemById(int id)
        {
            var result = await _mediator.Send(new GetMenuItemByIdQuery { Id = id });
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<MenuItemResponseDto>> CreateMenuItem([FromBody] CreateMenuItemDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new CreateMenuItemCommand
            {
                Name = dto.Name,
                Category = dto.Category,
                Price = dto.Price,
                IsAvailable = dto.IsAvailable
            };

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetMenuItemById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MenuItemResponseDto>> UpdateMenuItem(int id, [FromBody] UpdateMenuItemDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new UpdateMenuItemCommand
            {
                Id = id,
                Name = dto.Name,
                Category = dto.Category,
                Price = dto.Price,
                IsAvailable = dto.IsAvailable
            };

            var result = await _mediator.Send(command);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var result = await _mediator.Send(new DeleteMenuItemCommand { Id = id });
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
