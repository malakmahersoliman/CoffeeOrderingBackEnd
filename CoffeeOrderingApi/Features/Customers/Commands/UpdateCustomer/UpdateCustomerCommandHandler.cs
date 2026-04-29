

using CoffeeOrderingApi.DTOs;
using CoffeeOrderingApiWithCQRSandMediatR.Data;
using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerResponseDto?>

    {
        private readonly AppDbContext _context;
        public UpdateCustomerCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomerResponseDto?>
            Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {

            var customer = await _context.Customers.FindAsync(request.Id);
            if (customer == null)
            {
                return null;
            }
            customer.FullName = request.FullName;
            customer.PhoneNumber = request.PhoneNumber;
            await _context.SaveChangesAsync(cancellationToken);
            return new CustomerResponseDto
            {
                Id = customer.Id,
                FullName = customer.FullName,
                PhoneNumber = customer.PhoneNumber
            };
        }
    }
}
