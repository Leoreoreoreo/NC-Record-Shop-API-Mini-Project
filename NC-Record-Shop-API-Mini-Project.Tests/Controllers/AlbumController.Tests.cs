using Moq;
using Microsoft.AspNetCore.Mvc;
using NC_Record_Shop_API_Mini_Project.Controllers;
using NC_Record_Shop_API_Mini_Project.Services;
using NC_Record_Shop_API_Mini_Project.Models;
namespace NC_Record_Shop_API_Mini_Project.Tests;

public class AlbumControllerTests
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
    [Fact]
    public void GetAlbumById_ValidId_ShouldReturnOk()
    {
        var album = new Album { Id = 1, Name = "Abbey Road", Artist = "The Beatles", Genre = "Rock", ReleaseYear = 1969, Stock = 5 };
        var mockService = new Mock<IAlbumService>();
        mockService.Setup(r => r.GetAlbumById(1)).Returns(album);
        var controller = new AlbumController(mockService.Object);
        var result = controller.GetAlbumById(1);

        Assert.IsType<OkObjectResult>(result);
    }
    [Fact]
    public void GetAlbumById_InValidId_ShouldReturnNotFound()
    {
        var mockService = new Mock<IAlbumService>();
        mockService.Setup(r => r.GetAlbumById(100)).Returns((Album)null);
        var controller = new AlbumController(mockService.Object);
        var result = controller.GetAlbumById(100);

        Assert.IsType<NotFoundResult>(result);
    }
    [Fact]
    public void AddAlbum_ValidAlbum_ShouldReturnCreatedAlbum()
    {
        var album = new Album { Id = 1, Name = "Abbey Road", Artist = "The Beatles", Genre = "Rock", ReleaseYear = 1969, Stock = 5 };
        var mockService = new Mock<IAlbumService>();
        mockService.Setup(r => r.AddAlbum(album)).Returns(album);
        var controller = new AlbumController(mockService.Object);
        var result = controller.AddAlbum(album);

        Assert.IsType<CreatedAtActionResult>(result);
    }
}
