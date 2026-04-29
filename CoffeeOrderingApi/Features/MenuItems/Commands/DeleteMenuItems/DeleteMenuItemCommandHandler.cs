using CoffeeOrderingApiWithCQRSandMediatR.Data;
using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Commands.DeleteMenuItems
{
    public class DeleteMenuItemCommandHandler : IRequestHandler<DeleteMenuItemCommand, bool>
    {
        private readonly AppDbContext _context;
        public DeleteMenuItemCommandHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
        {
            var menuItem = await _context.MenuItems.FindAsync(request.Id);
            if (menuItem == null)
            {
                return false;
            }
            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

    }
}
