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
    [Fact]
    public void GetAlbumById_ValidId_ShouldReturnAlbum()
    {
        var album = new Album { Id = 1, Name = "Abbey Road", Artist = "The Beatles", Genre = "Rock", ReleaseYear = 1969, Stock = 5 };
        var mockRepository = new Mock<IAlbumRepository>();
        mockRepository.Setup(r => r.GetAlbumById(1)).Returns(album);
        var serivce = new AlbumService(mockRepository.Object);
        var result = serivce.GetAlbumById(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }
    [Fact]
    public void GetAlbumById_InValidId_ShouldReturnNull()
    {
        var mockRepository = new Mock<IAlbumRepository>();
        mockRepository.Setup(r => r.GetAlbumById(100)).Returns((Album)null);
        var serivce = new AlbumService(mockRepository.Object);
        var result = serivce.GetAlbumById(100);

        Assert.Null(result);
    }
    [Fact]
    public void AddAlbum_ValidAlbum_ShouldReturnCreatedAlbum()
    {
        var album = new Album { Id = 1, Name = "Abbey Road", Artist = "The Beatles", Genre = "Rock", ReleaseYear = 1969, Stock = 5 };
        var mockRepository = new Mock<IAlbumRepository>();
        mockRepository.Setup(r => r.AddAlbum(album)).Returns(album);
        var serivce = new AlbumService(mockRepository.Object);
        var result = serivce.AddAlbum(album);

        Assert.NotNull(result);
        Assert.True(result.Id > 0);
    }

    [Fact]
    public void UpdateAlbum_ValidId_ShouldReturnUpdatedAlbum()
    {
        var album = new Album { Id = 1, Name = "Let It Be", Artist = "The Beatles", Genre = "Rock", ReleaseYear = 1970, Stock = 3 };
        var mockRepository = new Mock<IAlbumRepository>();
        mockRepository.Setup(r => r.UpdateAlbum(1, album)).Returns(album);
        var service = new AlbumService(mockRepository.Object);

        var result = service.UpdateAlbum(1, album);

        Assert.NotNull(result);
        Assert.Equal("Let It Be", result.Name);
    }

    [Fact]
    public void UpdateAlbum_InvalidId_ShouldReturnNull()
    {
        var album = new Album { Id = 1, Name = "Let It Be", Artist = "The Beatles", Genre = "Rock", ReleaseYear = 1970, Stock = 3 };
        var mockRepository = new Mock<IAlbumRepository>();
        mockRepository.Setup(r => r.UpdateAlbum(100, album)).Returns((Album)null);
        var service = new AlbumService(mockRepository.Object);

        var result = service.UpdateAlbum(100, album);

        Assert.Null(result);
    }

    [Fact]
    public void DeleteAlbum_ValidId_ShouldReturnTrue()
    {
        var mockRepository = new Mock<IAlbumRepository>();
        mockRepository.Setup(r => r.DeleteAlbum(1)).Returns(true);
        var service = new AlbumService(mockRepository.Object);

        var result = service.DeleteAlbum(1);

        Assert.True(result);
    }

    [Fact]
    public void DeleteAlbum_InvalidId_ShouldReturnFalse()
    {
        var mockRepository = new Mock<IAlbumRepository>();
        mockRepository.Setup(r => r.DeleteAlbum(100)).Returns(false);
        var service = new AlbumService(mockRepository.Object);

        var result = service.DeleteAlbum(100);

        Assert.False(result);
    }
}
