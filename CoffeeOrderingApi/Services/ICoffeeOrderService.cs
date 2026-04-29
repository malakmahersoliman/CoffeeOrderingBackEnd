using CoffeeOrderingApiWithCQRSandMediatR.DTOs;

namespace CoffeeOrderingApiWithCQRSandMediatR.Services
{
    public interface ICoffeeOrderService
    {
        Task<List<CoffeeOrderResponseDto>> GetAll();
        Task<CoffeeOrderResponseDto?> GetById(int id);
        Task<CoffeeOrderResponseDto?> Create(CreateCoffeeOrderDto dto);
        Task<bool> Delete(int id);
    }
}