using CoffeeOrderingApi.DTOs;
using CoffeeOrderingApiWithCQRSandMediatR.DTOs;

namespace CoffeeOrderingApiWithCQRSandMediatR.Services
{
    public interface IMenuItemService
    {
        Task<IEnumerable<MenuItemResponseDto>> GetAllAsync();
        Task<MenuItemResponseDto?> GetByIdAsync(int id);
        Task<MenuItemResponseDto> CreateAsync(CreateMenuItemDto dto);
        Task<MenuItemResponseDto?> UpdateAsync(int id, UpdateMenuItemDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
