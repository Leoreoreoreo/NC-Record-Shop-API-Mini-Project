using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using NC_Record_Shop_API_Mini_Project.Controllers;
using NC_Record_Shop_API_Mini_Project.Models;
using NC_Record_Shop_API_Mini_Project.Services;
namespace NC_Record_Shop_API_Mini_Project.Tests;

public class OrderControllerTests
{
    private static OrderController CreateController(IOrderService service, string userId)
    {
        var controller = new OrderController(service);
        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userId) });
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal(identity) }
        };
        return controller;
    }

    [Fact]
    public void Checkout_Success_ShouldReturnCreated()
    {
        var request = new OrderRequest { Items = new List<OrderItemRequest> { new() { AlbumId = 1, Quantity = 1 } } };
        var mockService = new Mock<IOrderService>();
        mockService.Setup(s => s.Checkout("user1", request.Items)).Returns(new CheckoutResult { Success = true, Order = new Order() });
        var controller = CreateController(mockService.Object, "user1");

        var result = controller.Checkout(request);

        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public void Checkout_Failure_ShouldReturnBadRequest()
    {
        var request = new OrderRequest { Items = new List<OrderItemRequest> { new() { AlbumId = 1, Quantity = 99 } } };
        var mockService = new Mock<IOrderService>();
        mockService.Setup(s => s.Checkout("user1", request.Items)).Returns(new CheckoutResult { Success = false, Error = "Not enough stock." });
        var controller = CreateController(mockService.Object, "user1");

        var result = controller.Checkout(request);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void GetMyOrders_ShouldReturnOk()
    {
        var mockService = new Mock<IOrderService>();
        mockService.Setup(s => s.GetOrdersForUser("user1")).Returns(new List<Order>());
        var controller = CreateController(mockService.Object, "user1");

        var result = controller.GetMyOrders();

        Assert.IsType<OkObjectResult>(result);
    }
}
