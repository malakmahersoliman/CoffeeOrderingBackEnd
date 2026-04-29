using MediatR;

namespace CoffeeOrderingApiWithCQRSandMediatR.Features.MenuItems.Commands.DeleteMenuItems
{
    public class DeleteMenuItemCommand : IRequest<bool>
    {
      public int Id { get; set; }
    }
}
