using CoffeeOrderingApi.DTOs;
using CoffeeOrderingApiWithCQRSandMediatR.Data;
using CoffeeOrderingApiWithCQRSandMediatR.Domain;
using Microsoft.EntityFrameworkCore;

namespace CoffeeOrderingApiWithCQRSandMediatR.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;

        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerResponseDto>> GetAllAsync()
        {
            return await _context.Customers
                .Select(c => new CustomerResponseDto
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    PhoneNumber = c.PhoneNumber
                })
                .ToListAsync();
        }

        public async Task<CustomerResponseDto?> GetByIdAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return null;

            return new CustomerResponseDto
            {
                Id = customer.Id,
                FullName = customer.FullName,
                PhoneNumber = customer.PhoneNumber
            };
        }

        public async Task<CustomerResponseDto> CreateAsync(CreateCustomerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
            {
                throw new ArgumentException("Customer name cannot be empty or whitespace.", nameof(dto.FullName));
            }
            if (string.IsNullOrWhiteSpace(dto.PhoneNumber))
            {
                throw new ArgumentException("Phone number cannot be empty or whitespace.", nameof(dto.PhoneNumber));
            }

            var customer = new Customer
            {
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return new CustomerResponseDto
            {
                Id = customer.Id,
                FullName = customer.FullName,
                PhoneNumber = customer.PhoneNumber
            };
        }

        public async Task<CustomerResponseDto?> UpdateAsync(int id, UpdateCustomerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
            {
                throw new ArgumentException("Customer name cannot be empty or whitespace.", nameof(dto.FullName));
            }
            if (string.IsNullOrWhiteSpace(dto.PhoneNumber))
            {
                throw new ArgumentException("Phone number cannot be empty or whitespace.", nameof(dto.PhoneNumber));
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return null;

            customer.FullName = dto.FullName;
            customer.PhoneNumber = dto.PhoneNumber;

            await _context.SaveChangesAsync();

            return new CustomerResponseDto
            {
                Id = customer.Id,
                FullName = customer.FullName,
                PhoneNumber = customer.PhoneNumber
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null) return false;

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
