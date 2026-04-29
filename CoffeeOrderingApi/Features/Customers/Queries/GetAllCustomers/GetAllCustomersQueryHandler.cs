using CoffeeOrderingApi.DTOs;
using CoffeeOrderingApiWithCQRSandMediatR.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQueryHandler :
        IRequestHandler<GetAllCustomersQuery, List<CustomerResponseDto>>
    {
        private readonly AppDbContext _context;
        public GetAllCustomersQueryHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<CustomerResponseDto>> Handle
            (GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
         return await _context.Customers.Select(c => new CustomerResponseDto
            {
                Id = c.Id,
                FullName = c.FullName,
                PhoneNumber = c.PhoneNumber
            })
                .ToListAsync(cancellationToken);
        }
    }
}
