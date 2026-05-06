using Moq;
using NC_Record_Shop_API_Mini_Project.Services;
using NC_Record_Shop_API_Mini_Project.Models;
using NC_Record_Shop_API_Mini_Project.Repositories;
namespace NC_Record_Shop_API_Mini_Project.Tests;

public class AlbumServiceTests
{
    [Fact]
    public void GetAllAlbums_MockRepository_ShouldReturnOk()
    {
        var mockRepository = new Mock<IAlbumRepository>();
        mockRepository.Setup(r => r.GetAllAlbums()).Returns(
            new List<Album>());
        var service = new AlbumService(mockRepository.Object);
        var result = service.GetAllAlbums();
        Assert.IsType<List<Album>>(result);
    }
}
