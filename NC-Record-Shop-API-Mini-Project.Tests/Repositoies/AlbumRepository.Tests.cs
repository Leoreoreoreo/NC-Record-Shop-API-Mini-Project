using Microsoft.EntityFrameworkCore;
using NC_Record_Shop_API_Mini_Project.Data;
using NC_Record_Shop_API_Mini_Project.Models;
using NC_Record_Shop_API_Mini_Project.Repositories;
namespace NC_Record_Shop_API_Mini_Project.Tests;

public class AlbumRepositoryTests
{
    private AppDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public void GetAllAlbums_ShouldReturnAllAlbums()
    {
        var context = CreateInMemoryContext();
        var repository = new AlbumRepository(context);

        var result = repository.GetAllAlbums();

        Assert.IsType<List<Album>>(result);
    }
    [Fact]
    public void GetAlbumById_ValidId_ShouldReturnAlbum()
    {
        var context = CreateInMemoryContext();
        var album = new Album { Name = "Abbey Road", Artist = "The Beatles", Genre = "Rock", ReleaseYear = 1969, Stock = 5 };
        context.Albums.Add(album);
        context.SaveChanges();

        var repository = new AlbumRepository(context);
        var result = repository.GetAlbumById(album.Id);

        Assert.NotNull(result);
        Assert.Equal(album.Id, result.Id);
    }
    [Fact]
    public void GetAlbumById_InValidId_ShouldReturnNull()
    {
        var context = CreateInMemoryContext();
        var album = new Album { Name = "Abbey Road", Artist = "The Beatles", Genre = "Rock", ReleaseYear = 1969, Stock = 5 };
        context.Albums.Add(album);
        context.SaveChanges();

        var repository = new AlbumRepository(context);
        var result = repository.GetAlbumById(100);

        Assert.Null(result);
    }
    [Fact]
    public void AddAlbum_ValidAlbum_ShouldReturnCreatedAlbum()
    {
        var context = CreateInMemoryContext();
        var repository = new AlbumRepository(context);
        var album = new Album { Name = "Abbey Road", Artist = "The Beatles", Genre = "Rock", ReleaseYear = 1969, Stock = 5 };

        var result = repository.AddAlbum(album);

        Assert.NotNull(result);
        Assert.True(result.Id > 0);
    }
    [Fact]
    public void UpdateAlbum_ValidId_ShouldReturnUpdatedAlbum()
    {
        var context = CreateInMemoryContext();
        var album = new Album { Name = "Abbey Road", Artist = "The Beatles", Genre = "Rock", ReleaseYear = 1969, Stock = 5 };
        context.Albums.Add(album);
        context.SaveChanges();

        var repository = new AlbumRepository(context);
        var updatedAlbum = new Album { Name = "Let It Be", Artist = "The Beatles", Genre = "Rock", ReleaseYear = 1970, Stock = 3 };
        var result = repository.UpdateAlbum(album.Id, updatedAlbum);

        Assert.NotNull(result);
        Assert.Equal("Let It Be", result.Name);
    }

    [Fact]
    public void UpdateAlbum_InvalidId_ShouldReturnNull()
    {
        var context = CreateInMemoryContext();
        var repository = new AlbumRepository(context);
        var updatedAlbum = new Album { Name = "Let It Be", Artist = "The Beatles", Genre = "Rock", ReleaseYear = 1970, Stock = 3 };

        var result = repository.UpdateAlbum(100, updatedAlbum);

        Assert.Null(result);
    }

    [Fact]
    public void DeleteAlbum_ValidId_ShouldReturnTrue()
    {
        var context = CreateInMemoryContext();
        var album = new Album { Name = "Abbey Road", Artist = "The Beatles", Genre = "Rock", ReleaseYear = 1969, Stock = 5 };
        context.Albums.Add(album);
        context.SaveChanges();

        var repository = new AlbumRepository(context);
        var result = repository.DeleteAlbum(album.Id);

        Assert.True(result);
        Assert.Null(context.Albums.FirstOrDefault(a => a.Id == album.Id));
    }

    [Fact]
    public void DeleteAlbum_InvalidId_ShouldReturnFalse()
    {
        var context = CreateInMemoryContext();
        var repository = new AlbumRepository(context);

        var result = repository.DeleteAlbum(100);

        Assert.False(result);
    }
}