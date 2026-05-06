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

}