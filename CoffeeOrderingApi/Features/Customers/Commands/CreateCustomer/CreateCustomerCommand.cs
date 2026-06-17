using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<CustomerResponseDto>
    {
        public string FullName { get; set; } = "";

        public string PhoneNumber { get; set; } = "";
    }
}
