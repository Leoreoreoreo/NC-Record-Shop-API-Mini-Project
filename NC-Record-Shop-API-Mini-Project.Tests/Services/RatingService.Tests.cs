using Moq;
using NC_Record_Shop_API_Mini_Project.Models;
using NC_Record_Shop_API_Mini_Project.Repositories;
using NC_Record_Shop_API_Mini_Project.Services;
namespace NC_Record_Shop_API_Mini_Project.Tests;

public class RatingServiceTests
{
    [Fact]
    public void AddRating_ShouldReturnRatingFromRepository()
    {
        var rating = new Rating { Id = 1, AlbumId = 1, Stars = 4 };
        var mockRepository = new Mock<IRatingRepository>();
        mockRepository.Setup(r => r.AddRating(1, 4)).Returns(rating);
        var service = new RatingService(mockRepository.Object);

        var result = service.AddRating(1, 4);

        Assert.NotNull(result);
        Assert.Equal(4, result.Stars);
    }

    [Fact]
    public void AddRating_InvalidAlbum_ShouldReturnNull()
    {
        var mockRepository = new Mock<IRatingRepository>();
        mockRepository.Setup(r => r.AddRating(100, 4)).Returns((Rating?)null);
        var service = new RatingService(mockRepository.Object);

        var result = service.AddRating(100, 4);

        Assert.Null(result);
    }

    [Fact]
    public void GetSummary_ShouldReturnSummaryFromRepository()
    {
        var summary = new RatingSummary { AlbumId = 1, AverageStars = 3.5, Count = 2 };
        var mockRepository = new Mock<IRatingRepository>();
        mockRepository.Setup(r => r.GetSummary(1)).Returns(summary);
        var service = new RatingService(mockRepository.Object);

        var result = service.GetSummary(1);

        Assert.Same(summary, result);
    }
}
