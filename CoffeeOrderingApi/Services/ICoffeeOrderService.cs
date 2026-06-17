using CoffeeOrderingApiWithCQRSandMediatR.DTOs;

namespace CoffeeOrderingApiWithCQRSandMediatR.Services
{
    public interface ICoffeeOrderService
    {
        Task<IEnumerable<CoffeeOrderResponseDto>> GetAllAsync();
        Task<CoffeeOrderResponseDto?> GetByIdAsync(int id);
        Task<CoffeeOrderResponseDto?> CreateAsync(CreateCoffeeOrderDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
