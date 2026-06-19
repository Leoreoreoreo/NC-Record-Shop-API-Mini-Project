using Moq;
using NC_Record_Shop_API_Mini_Project.Models;
using NC_Record_Shop_API_Mini_Project.Repositories;
using NC_Record_Shop_API_Mini_Project.Services;
namespace NC_Record_Shop_API_Mini_Project.Tests;

public class OrderServiceTests
{
    [Fact]
    public void Checkout_ShouldReturnResultFromRepository()
    {
        var checkoutResult = new CheckoutResult { Success = true, Order = new Order() };
        var items = new List<OrderItemRequest> { new() { AlbumId = 1, Quantity = 1 } };
        var mockRepository = new Mock<IOrderRepository>();
        mockRepository.Setup(r => r.Checkout("user1", items)).Returns(checkoutResult);
        var service = new OrderService(mockRepository.Object);

        var result = service.Checkout("user1", items);

        Assert.Same(checkoutResult, result);
    }

    [Fact]
    public void GetOrdersForUser_ShouldReturnOrdersFromRepository()
    {
        var orders = new List<Order> { new() { UserId = "user1" } };
        var mockRepository = new Mock<IOrderRepository>();
        mockRepository.Setup(r => r.GetOrdersForUser("user1")).Returns(orders);
        var service = new OrderService(mockRepository.Object);

        var result = service.GetOrdersForUser("user1");

        Assert.Same(orders, result);
    }
}
