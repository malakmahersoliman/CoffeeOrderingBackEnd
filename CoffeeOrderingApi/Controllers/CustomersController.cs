using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using CoffeeOrderingApiWithCQRSandMediatR.Features.Customers.Commands.CreateCustomer;
using CoffeeOrderingApiWithCQRSandMediatR.Features.Customers.Commands.UpdateCustomer;
using CoffeeOrderingApiWithCQRSandMediatR.Features.Customers.Commands.DeleteCustomer;
using CoffeeOrderingApiWithCQRSandMediatR.Features.Customers.Queries.GetAllCustomers;
using CoffeeOrderingApiWithCQRSandMediatR.Features.Customers.Queries.GetCustomerById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeOrderingApiWithCQRSandMediatR.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetAllCustomers()
        {
            var result = await _mediator.Send(new GetAllCustomersQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponseDto>> GetCustomerById(int id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery { Id = id });
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerResponseDto>> CreateCustomer([FromBody] CreateCustomerDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new CreateCustomerCommand
            {
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber
            };

            var customer = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerResponseDto>> UpdateCustomer(int id, [FromBody] UpdateCustomerDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new UpdateCustomerCommand
            {
                Id = id,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber
            };

            var updatedCustomer = await _mediator.Send(command);
            if (updatedCustomer == null)
            {
                return NotFound();
            }
            return Ok(updatedCustomer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand { Id = id });
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
