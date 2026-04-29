using CoffeeOrderingApi.DTOs;
using MediatR;


namespace CoffeeOrderingApiWithCQRSandMediatR.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<CustomerResponseDto?>
    {
        public int Id{ get; set; }

        public string FullName { get; set; } = "";

        public string PhoneNumber { get; set; } = "";


    }
}
