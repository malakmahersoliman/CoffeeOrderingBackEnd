using CoffeeOrderingApi.DTOs;
using CoffeeOrderingApiWithCQRSandMediatR.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerResponseDto?>
    {
        private readonly AppDbContext _context;

        public GetCustomerByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerResponseDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customers
                .Where(c => c.Id == request.Id)
                .Select(c => new CustomerResponseDto
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    PhoneNumber = c.PhoneNumber
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
