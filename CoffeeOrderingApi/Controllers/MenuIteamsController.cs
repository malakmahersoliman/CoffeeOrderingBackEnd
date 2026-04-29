using CoffeeOrderingApi.DTOs;
using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Commands.CreateMenuItems;
using CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Commands.DeleteMenuItems;
using CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Commands.UpdateMenuItems;
using CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Queries;
using CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Queries.GetAllMenuItems;
using CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Queries.GetMenuItemsById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeOrderingApiWithCQRSandMediatR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuIteamsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MenuIteamsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMenuItems()
        {
            var result = await _mediator.Send(new GetAllMenuItemsQuery());
            return Ok(result);

        }
        [HttpPost]
        public async Task<IActionResult> CreateMenuItem(CreateMenuItemsCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetAllMenuItems), new { id = result.Id }, result);

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMenuItem(int id, UpdateMenuItemDto command)
        {
            var result = await _mediator.Send(new UpdateMenuItemsCommand() { Id = id, Name = command.Name, Category = command.Category, Price = command.Price, IsAvailable = command.IsAvailable });
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var result = await _mediator.Send(new DeleteMenuItemCommand() { Id = id });
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuItemById(int id)
        {
            var result = await _mediator.Send(new GetMenuItemByIdQuery() { Id = id });
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}