using System.Net;
using System.Net.Http.Json;
using CoffeeOrderingApiWithCQRSandMediatR.DTOs;

namespace CoffeeOrderingApi.Tests;

public class CustomersApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CustomersApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateCustomer_ReturnsCreated()
    {
        var response = await _client.PostAsJsonAsync("/api/Customers", new CreateCustomerDto
        {
            FullName = "Test User",
            PhoneNumber = "555-1234"
        });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var customer = await response.Content.ReadFromJsonAsync<CustomerResponseDto>();
        Assert.NotNull(customer);
        Assert.Equal("Test User", customer.FullName);
    }

    [Fact]
    public async Task GetCustomerById_WhenMissing_ReturnsNotFound()
    {
        var response = await _client.GetAsync("/api/Customers/99999");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
