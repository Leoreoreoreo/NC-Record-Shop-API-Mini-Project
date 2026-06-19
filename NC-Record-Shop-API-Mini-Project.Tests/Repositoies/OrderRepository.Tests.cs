using Microsoft.EntityFrameworkCore;
using NC_Record_Shop_API_Mini_Project.Data;
using NC_Record_Shop_API_Mini_Project.Models;
using NC_Record_Shop_API_Mini_Project.Repositories;
namespace NC_Record_Shop_API_Mini_Project.Tests;

public class OrderRepositoryTests
{
    private RecordShopDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<RecordShopDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new RecordShopDbContext(options);
    }

    private Album AddAlbum(RecordShopDbContext context, string name, int stock, decimal price)
    {
        var album = new Album { Name = name, Artist = "X", Genre = "Rock", ReleaseYear = 2000, Stock = stock, Price = price };
        context.Albums.Add(album);
        context.SaveChanges();
        return album;
    }

    [Fact]
    public void Checkout_ValidItems_ShouldCreateOrderAndDecrementStock()
    {
        var context = CreateInMemoryContext();
        var album = AddAlbum(context, "Thriller", 5, 10.00m);
        var repository = new OrderRepository(context);

        var result = repository.Checkout("user1", new List<OrderItemRequest> { new() { AlbumId = album.Id, Quantity = 2 } });

        Assert.True(result.Success);
        Assert.NotNull(result.Order);
        Assert.Single(result.Order!.Items);
        Assert.Equal(20.00m, result.Order.Total);
        Assert.Equal(3, context.Albums.First(a => a.Id == album.Id).Stock);
    }

    [Fact]
    public void Checkout_InsufficientStock_ShouldFailAndNotChangeStock()
    {
        var context = CreateInMemoryContext();
        var album = AddAlbum(context, "Thriller", 1, 10.00m);
        var repository = new OrderRepository(context);

        var result = repository.Checkout("user1", new List<OrderItemRequest> { new() { AlbumId = album.Id, Quantity = 5 } });

        Assert.False(result.Success);
        Assert.NotNull(result.Error);
        Assert.Equal(1, context.Albums.First(a => a.Id == album.Id).Stock);
        Assert.Empty(context.Orders);
    }

    [Fact]
    public void Checkout_MissingAlbum_ShouldFail()
    {
        var context = CreateInMemoryContext();
        var repository = new OrderRepository(context);

        var result = repository.Checkout("user1", new List<OrderItemRequest> { new() { AlbumId = 999, Quantity = 1 } });

        Assert.False(result.Success);
        Assert.Empty(context.Orders);
    }

    [Fact]
    public void GetOrdersForUser_ShouldReturnOnlyThatUsersOrders()
    {
        var context = CreateInMemoryContext();
        var album = AddAlbum(context, "Thriller", 10, 10.00m);
        var repository = new OrderRepository(context);
        repository.Checkout("user1", new List<OrderItemRequest> { new() { AlbumId = album.Id, Quantity = 1 } });
        repository.Checkout("user2", new List<OrderItemRequest> { new() { AlbumId = album.Id, Quantity = 1 } });

        var result = repository.GetOrdersForUser("user1");

        Assert.Single(result);
        Assert.Equal("user1", result[0].UserId);
    }
}
