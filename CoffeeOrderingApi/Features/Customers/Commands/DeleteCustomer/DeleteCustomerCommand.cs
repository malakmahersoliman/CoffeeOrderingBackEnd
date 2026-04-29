using CoffeeOrderingApi.DTOs;
using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<bool>
    {
        public int Id { get; set; }

    }
}
