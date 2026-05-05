using Moq;
using Microsoft.AspNetCore.Mvc;
using NC_Record_Shop_API_Mini_Project.Controllers;
using NC_Record_Shop_API_Mini_Project.Services;
using NC_Record_Shop_API_Mini_Project.Models;
namespace NC_Record_Shop_API_Mini_Project.Tests;

public class ControllersTest
{
    [Fact]
    public void GetAllAlbums_MockService_ShouldReturnOk()
    {
        var mockService = new Mock<IAlbumService>();
        mockService.Setup(s => s.GetAllAlbums()).Returns(
            new List<Album>());
        var controller = new AlbumController(mockService.Object);
        var result = controller.GetAllAlbums();
        Assert.IsType<OkObjectResult>(result);
    }
}
