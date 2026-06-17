using CoffeeOrderingApi.DTOs;
using CoffeeOrderingApiWithCQRSandMediatR.Data;
using CoffeeOrderingApiWithCQRSandMediatR.Domain;
using CoffeeOrderingApiWithCQRSandMediatR.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CoffeeOrderingApiWithCQRSandMediatR.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly AppDbContext _context;

        public MenuItemService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuItemResponseDto>> GetAllAsync()
        {
            return await _context.MenuItems
                .Select(m => new MenuItemResponseDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Category = m.Category,
                    Price = m.Price,
                    IsAvailable = m.IsAvailable
                })
                .ToListAsync();
        }

        public async Task<MenuItemResponseDto?> GetByIdAsync(int id)
        {
            var item = await _context.MenuItems.FindAsync(id);
            if (item == null) return null;

            return new MenuItemResponseDto
            {
                Id = item.Id,
                Name = item.Name,
                Category = item.Category,
                Price = item.Price,
                IsAvailable = item.IsAvailable
            };
        }

        public async Task<MenuItemResponseDto> CreateAsync(CreateMenuItemDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("MenuItem name cannot be empty or whitespace.", nameof(dto.Name));
            }
            if (string.IsNullOrWhiteSpace(dto.Category))
            {
                throw new ArgumentException("Category cannot be empty or whitespace.", nameof(dto.Category));
            }
            if (dto.Price <= 0)
            {
                throw new ArgumentException("Price must be greater than zero.", nameof(dto.Price));
            }

            var item = new MenuItem
            {
                Name = dto.Name,
                Category = dto.Category,
                Price = dto.Price,
                IsAvailable = dto.IsAvailable
            };

            _context.MenuItems.Add(item);
            await _context.SaveChangesAsync();

            return new MenuItemResponseDto
            {
                Id = item.Id,
                Name = item.Name,
                Category = item.Category,
                Price = item.Price,
                IsAvailable = item.IsAvailable
            };
        }

        public async Task<MenuItemResponseDto?> UpdateAsync(int id, UpdateMenuItemDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("MenuItem name cannot be empty or whitespace.", nameof(dto.Name));
            }
            if (string.IsNullOrWhiteSpace(dto.Category))
            {
                throw new ArgumentException("Category cannot be empty or whitespace.", nameof(dto.Category));
            }
            if (dto.Price <= 0)
            {
                throw new ArgumentException("Price must be greater than zero.", nameof(dto.Price));
            }

            var item = await _context.MenuItems.FindAsync(id);
            if (item == null) return null;

            item.Name = dto.Name;
            item.Category = dto.Category;
            item.Price = dto.Price;
            item.IsAvailable = dto.IsAvailable;

            await _context.SaveChangesAsync();

            return new MenuItemResponseDto
            {
                Id = item.Id,
                Name = item.Name,
                Category = item.Category,
                Price = item.Price,
                IsAvailable = item.IsAvailable
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.MenuItems.FindAsync(id);
            if (item == null) return false;

            _context.MenuItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
