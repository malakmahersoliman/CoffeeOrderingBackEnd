using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Commands.DeleteMenuItem
{
    public class DeleteMenuItemCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
