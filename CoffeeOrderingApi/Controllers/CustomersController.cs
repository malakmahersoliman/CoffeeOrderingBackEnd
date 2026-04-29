using CoffeeOrderingApi.DTOs;
using CoffeeOrderingApiWithCQRSandMediatR.Features.Customers.Commands.CreateCustomer;
using CoffeeOrderingApiWithCQRSandMediatR.Features.Customers.Commands.DeleteCustomer;
using CoffeeOrderingApiWithCQRSandMediatR.Features.Customers.Commands.UpdateCustomer;
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
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _mediator.Send(new GetAllCustomersQuery());
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerCommand command)
        {
            var customer = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAllCustomers), new { id = customer.Id }, customer);
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
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerDto command)
        {

            var updatedCustomer = await _mediator.Send(new UpdateCustomerCommand() { Id = id, FullName = command.FullName, PhoneNumber = command.PhoneNumber });
            if (updatedCustomer == null)
            {
                return NotFound();
            }
            return Ok(updatedCustomer);
        }
    
    [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery { Id = id });
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}

