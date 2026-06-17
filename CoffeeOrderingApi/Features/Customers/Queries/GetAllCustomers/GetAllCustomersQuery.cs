using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.Customers.Queries.GetAllCustomers
{
    public class GetAllCustomersQuery: IRequest<List<CustomerResponseDto>>
    {
    }
}
