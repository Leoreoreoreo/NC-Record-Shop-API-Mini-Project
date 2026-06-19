using Microsoft.EntityFrameworkCore;
using NC_Record_Shop_API_Mini_Project.Data;
using NC_Record_Shop_API_Mini_Project.Models;
using NC_Record_Shop_API_Mini_Project.Repositories;
namespace NC_Record_Shop_API_Mini_Project.Tests;

public class AlbumRepositoryTests
{
    private RecordShopDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<RecordShopDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new RecordShopDbContext(options);
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

    private RecordShopDbContext CreateContextWithSampleAlbums()
    {
        var context = CreateInMemoryContext();
        context.Albums.AddRange(
            new Album { Name = "Abbey Road", Artist = "The Beatles", Genre = "Rock", ReleaseYear = 1969, Stock = 5 },
            new Album { Name = "Let It Be", Artist = "The Beatles", Genre = "Rock", ReleaseYear = 1970, Stock = 3 },
            new Album { Name = "Kind of Blue", Artist = "Miles Davis", Genre = "Jazz", ReleaseYear = 1959, Stock = 4 }
        );
        context.SaveChanges();
        return context;
    }

    [Fact]
    public void GetFilteredAlbums_NoFilters_ShouldReturnAllAlbums()
    {
        var context = CreateContextWithSampleAlbums();
        var repository = new AlbumRepository(context);

        var result = repository.GetFilteredAlbums(null, null, null, null);

        Assert.Equal(3, result.Count);
    }

    [Fact]
    public void GetFilteredAlbums_ByArtist_ShouldReturnMatchingAlbums()
    {
        var context = CreateContextWithSampleAlbums();
        var repository = new AlbumRepository(context);

        var result = repository.GetFilteredAlbums("beatles", null, null, null);

        Assert.Equal(2, result.Count);
        Assert.All(result, a => Assert.Equal("The Beatles", a.Artist));
    }

    [Fact]
    public void GetFilteredAlbums_ByGenre_ShouldReturnMatchingAlbums()
    {
        var context = CreateContextWithSampleAlbums();
        var repository = new AlbumRepository(context);

        var result = repository.GetFilteredAlbums(null, "Jazz", null, null);

        Assert.Single(result);
        Assert.Equal("Kind of Blue", result[0].Name);
    }

    [Fact]
    public void GetFilteredAlbums_ByReleaseYear_ShouldReturnMatchingAlbums()
    {
        var context = CreateContextWithSampleAlbums();
        var repository = new AlbumRepository(context);

        var result = repository.GetFilteredAlbums(null, null, 1970, null);

        Assert.Single(result);
        Assert.Equal("Let It Be", result[0].Name);
    }

    [Fact]
    public void GetFilteredAlbums_ByNameSubstring_ShouldReturnMatchingAlbums()
    {
        var context = CreateContextWithSampleAlbums();
        var repository = new AlbumRepository(context);

        var result = repository.GetFilteredAlbums(null, null, null, "blue");

        Assert.Single(result);
        Assert.Equal("Kind of Blue", result[0].Name);
    }

    [Fact]
    public void GetFilteredAlbums_NoMatch_ShouldReturnEmptyList()
    {
        var context = CreateContextWithSampleAlbums();
        var repository = new AlbumRepository(context);

        var result = repository.GetFilteredAlbums("Nirvana", null, null, null);

        Assert.Empty(result);
    }

    [Fact]
    public void GetPagedAlbums_FirstPage_ShouldReturnRequestedPageAndTotals()
    {
        var context = CreateContextWithSampleAlbums();
        var repository = new AlbumRepository(context);

        var result = repository.GetPagedAlbums(null, null, null, null, 1, 2);

        Assert.Equal(2, result.Albums.Count);
        Assert.Equal(1, result.Page);
        Assert.Equal(2, result.PageSize);
        Assert.Equal(3, result.TotalCount);
        Assert.Equal(2, result.TotalPages);
    }

    [Fact]
    public void GetPagedAlbums_LastPage_ShouldReturnRemainingAlbums()
    {
        var context = CreateContextWithSampleAlbums();
        var repository = new AlbumRepository(context);

        var result = repository.GetPagedAlbums(null, null, null, null, 2, 2);

        Assert.Single(result.Albums);
        Assert.Equal(3, result.TotalCount);
    }

    [Fact]
    public void GetPagedAlbums_WithFilter_ShouldPaginateFilteredResults()
    {
        var context = CreateContextWithSampleAlbums();
        var repository = new AlbumRepository(context);

        var result = repository.GetPagedAlbums("beatles", null, null, null, 1, 1);

        Assert.Single(result.Albums);
        Assert.Equal("The Beatles", result.Albums[0].Artist);
        Assert.Equal(2, result.TotalCount);
        Assert.Equal(2, result.TotalPages);
    }
}