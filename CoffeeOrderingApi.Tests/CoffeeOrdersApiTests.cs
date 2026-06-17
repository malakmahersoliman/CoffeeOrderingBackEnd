using System.Net;
using System.Net.Http.Json;
using CoffeeOrderingApiWithCQRSandMediatR.DTOs;

namespace CoffeeOrderingApi.Tests;

public class CoffeeOrdersApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CoffeeOrdersApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateOrder_WithInvalidCustomer_ReturnsNotFound()
    {
        var response = await _client.PostAsJsonAsync("/api/CoffeeOrders", new CreateCoffeeOrderDto
        {
            CustomerId = 99999,
            IsTakeAway = false,
            Items = new List<CreateOrderItemDto>
            {
                new() { MenuItemId = 1, Quantity = 1 }
            }
        });

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task CreateOrder_WithEmptyItems_ReturnsBadRequest()
    {
        var customerResponse = await _client.PostAsJsonAsync("/api/Customers", new CreateCustomerDto
        {
            FullName = "Order Customer",
            PhoneNumber = "555-4321"
        });
        var customer = await customerResponse.Content.ReadFromJsonAsync<CustomerResponseDto>();
        Assert.NotNull(customer);

        var response = await _client.PostAsJsonAsync("/api/CoffeeOrders", new CreateCoffeeOrderDto
        {
            CustomerId = customer.Id,
            IsTakeAway = false,
            Items = new List<CreateOrderItemDto>()
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateAndUpdateOrder_WorksEndToEnd()
    {
        var customer = await (await _client.PostAsJsonAsync("/api/Customers", new CreateCustomerDto
        {
            FullName = "Coffee Lover",
            PhoneNumber = "555-0001"
        })).Content.ReadFromJsonAsync<CustomerResponseDto>();

        var menuItem = await (await _client.PostAsJsonAsync("/api/MenuItems", new CreateMenuItemDto
        {
            Name = "Espresso",
            Category = "Coffee",
            Price = 3.50m,
            IsAvailable = true
        })).Content.ReadFromJsonAsync<MenuItemResponseDto>();

        Assert.NotNull(customer);
        Assert.NotNull(menuItem);

        var createResponse = await _client.PostAsJsonAsync("/api/CoffeeOrders", new CreateCoffeeOrderDto
        {
            CustomerId = customer.Id,
            IsTakeAway = false,
            Items = new List<CreateOrderItemDto>
            {
                new() { MenuItemId = menuItem.Id, Quantity = 2 }
            }
        });

        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
        var order = await createResponse.Content.ReadFromJsonAsync<CoffeeOrderResponseDto>();
        Assert.NotNull(order);
        Assert.Equal(7.00m, order.TotalAmount);
        Assert.Equal("Pending", order.Status);

        var updateResponse = await _client.PutAsJsonAsync($"/api/CoffeeOrders/{order.Id}", new UpdateCoffeeOrderDto
        {
            Status = "Completed",
            IsTakeAway = true
        });

        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
        var updatedOrder = await updateResponse.Content.ReadFromJsonAsync<CoffeeOrderResponseDto>();
        Assert.NotNull(updatedOrder);
        Assert.Equal("Completed", updatedOrder.Status);
        Assert.True(updatedOrder.IsTakeAway);
    }
}
