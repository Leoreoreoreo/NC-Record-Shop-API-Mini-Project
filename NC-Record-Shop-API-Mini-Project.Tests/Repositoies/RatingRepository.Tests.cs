using Microsoft.EntityFrameworkCore;
using NC_Record_Shop_API_Mini_Project.Data;
using NC_Record_Shop_API_Mini_Project.Models;
using NC_Record_Shop_API_Mini_Project.Repositories;
namespace NC_Record_Shop_API_Mini_Project.Tests;

public class RatingRepositoryTests
{
    private RecordShopDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<RecordShopDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new RecordShopDbContext(options);
    }

    private Album AddAlbum(RecordShopDbContext context)
    {
        var album = new Album { Name = "Abbey Road", Artist = "The Beatles", Genre = "Rock", ReleaseYear = 1969, Stock = 5 };
        context.Albums.Add(album);
        context.SaveChanges();
        return album;
    }

    [Fact]
    public void AddRating_ValidAlbum_ShouldCreateRating()
    {
        var context = CreateInMemoryContext();
        var album = AddAlbum(context);
        var repository = new RatingRepository(context);

        var result = repository.AddRating(album.Id, 4);

        Assert.NotNull(result);
        Assert.Equal(album.Id, result.AlbumId);
        Assert.Equal(4, result.Stars);
    }

    [Fact]
    public void AddRating_InvalidAlbum_ShouldReturnNull()
    {
        var context = CreateInMemoryContext();
        var repository = new RatingRepository(context);

        var result = repository.AddRating(100, 4);

        Assert.Null(result);
    }

    [Fact]
    public void GetSummary_WithRatings_ShouldReturnAverageAndCount()
    {
        var context = CreateInMemoryContext();
        var album = AddAlbum(context);
        var repository = new RatingRepository(context);
        repository.AddRating(album.Id, 4);
        repository.AddRating(album.Id, 2);

        var summary = repository.GetSummary(album.Id);

        Assert.Equal(album.Id, summary.AlbumId);
        Assert.Equal(2, summary.Count);
        Assert.Equal(3.0, summary.AverageStars);
    }

    [Fact]
    public void GetSummary_NoRatings_ShouldReturnZero()
    {
        var context = CreateInMemoryContext();
        var album = AddAlbum(context);
        var repository = new RatingRepository(context);

        var summary = repository.GetSummary(album.Id);

        Assert.Equal(0, summary.Count);
        Assert.Equal(0.0, summary.AverageStars);
    }

    [Fact]
    public void AlbumExists_ShouldReflectWhetherAlbumIsPresent()
    {
        var context = CreateInMemoryContext();
        var album = AddAlbum(context);
        var repository = new RatingRepository(context);

        Assert.True(repository.AlbumExists(album.Id));
        Assert.False(repository.AlbumExists(999));
    }
}
