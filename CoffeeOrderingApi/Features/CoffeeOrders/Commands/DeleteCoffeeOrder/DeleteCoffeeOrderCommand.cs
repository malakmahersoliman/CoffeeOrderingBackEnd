using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.CoffeeOrders.Commands.DeleteCoffeeOrder
{
    public class DeleteCoffeeOrderCommand : IRequest<bool>
    {
        public int Id { get; }

        public DeleteCoffeeOrderCommand(int id)
        {
            Id = id;
        }
    }
}
