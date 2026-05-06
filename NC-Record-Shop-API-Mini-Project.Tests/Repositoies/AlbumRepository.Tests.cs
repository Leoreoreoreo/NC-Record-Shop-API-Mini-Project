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
}