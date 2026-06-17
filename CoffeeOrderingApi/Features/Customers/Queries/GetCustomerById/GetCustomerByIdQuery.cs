using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.Customers.Queries.GetCustomerById
{
    public class GetCustomerByIdQuery : IRequest<CustomerResponseDto?>
    {
        public int Id { get; set; }
    }
}
