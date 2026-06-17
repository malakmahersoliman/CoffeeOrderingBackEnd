using CoffeeOrderingApiWithCQRSandMediatR.Data;
using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using CoffeeOrderingApiWithCQRSandMediatR.Features.CoffeeOrders.Queries.GetCoffeeOrderById;
using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.CoffeeOrders.Commands.UpdateCoffeeOrder
{
    public class UpdateCoffeeOrderCommandHandler : IRequestHandler<UpdateCoffeeOrderCommand, CoffeeOrderResponseDto?>
    {
        private readonly AppDbContext _context;
        private readonly IMediator _mediator;

        public UpdateCoffeeOrderCommandHandler(AppDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<CoffeeOrderResponseDto?> Handle(UpdateCoffeeOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.CoffeeOrders.FindAsync(new object[] { request.Id }, cancellationToken);
            if (order == null)
            {
                return null;
            }

            order.Status = request.Status;
            order.IsTakeAway = request.IsTakeAway;
            await _context.SaveChangesAsync(cancellationToken);

            return await _mediator.Send(new GetCoffeeOrderByIdQuery(order.Id), cancellationToken);
        }
    }
}
