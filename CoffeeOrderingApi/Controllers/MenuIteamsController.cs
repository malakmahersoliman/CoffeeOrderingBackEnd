using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Commands;
using CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Queries;
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
        public async Task<IActionResult> CreateMenuItem(CreateMenuItemCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetAllMenuItems), new { id = result.Id }, result);

        }
    }

}