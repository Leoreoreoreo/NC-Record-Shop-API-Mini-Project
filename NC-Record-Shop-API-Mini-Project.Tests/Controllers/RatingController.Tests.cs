using Moq;
using Microsoft.AspNetCore.Mvc;
using NC_Record_Shop_API_Mini_Project.Controllers;
using NC_Record_Shop_API_Mini_Project.Models;
using NC_Record_Shop_API_Mini_Project.Services;
namespace NC_Record_Shop_API_Mini_Project.Tests;

public class RatingControllerTests
{
    [Fact]
    public void AddRating_ValidAlbum_ShouldReturnCreated()
    {
        var rating = new Rating { Id = 1, AlbumId = 1, Stars = 4 };
        var mockService = new Mock<IRatingService>();
        mockService.Setup(s => s.AddRating(1, 4)).Returns(rating);
        var controller = new RatingController(mockService.Object);

        var result = controller.AddRating(1, new RatingRequest { Stars = 4 });

        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public void AddRating_InvalidAlbum_ShouldReturnNotFound()
    {
        var mockService = new Mock<IRatingService>();
        mockService.Setup(s => s.AddRating(100, 4)).Returns((Rating?)null);
        var controller = new RatingController(mockService.Object);

        var result = controller.AddRating(100, new RatingRequest { Stars = 4 });

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public void GetRatings_ValidAlbum_ShouldReturnOk()
    {
        var mockService = new Mock<IRatingService>();
        mockService.Setup(s => s.AlbumExists(1)).Returns(true);
        mockService.Setup(s => s.GetSummary(1)).Returns(new RatingSummary { AlbumId = 1 });
        var controller = new RatingController(mockService.Object);

        var result = controller.GetRatings(1);

        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetRatings_InvalidAlbum_ShouldReturnNotFound()
    {
        var mockService = new Mock<IRatingService>();
        mockService.Setup(s => s.AlbumExists(100)).Returns(false);
        var controller = new RatingController(mockService.Object);

        var result = controller.GetRatings(100);

        Assert.IsType<NotFoundObjectResult>(result);
    }
}
