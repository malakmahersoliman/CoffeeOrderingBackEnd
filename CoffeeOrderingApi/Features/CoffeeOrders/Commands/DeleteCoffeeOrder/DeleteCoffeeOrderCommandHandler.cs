using CoffeeOrderingApiWithCQRSandMediatR.Data;
using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.CoffeeOrders.Commands.DeleteCoffeeOrder
{
    public class DeleteCoffeeOrderCommandHandler : IRequestHandler<DeleteCoffeeOrderCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteCoffeeOrderCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCoffeeOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.CoffeeOrders.FindAsync(new object[] { request.Id }, cancellationToken);
            if (order == null) return false;

            _context.CoffeeOrders.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
