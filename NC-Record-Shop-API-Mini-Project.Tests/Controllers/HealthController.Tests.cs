using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NC_Record_Shop_API_Mini_Project.Controllers;
using NC_Record_Shop_API_Mini_Project.Data;
namespace NC_Record_Shop_API_Mini_Project.Tests;

public class HealthControllerTests
{
    private AppDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public void GetHealth_DatabaseReachable_ShouldReturnOk()
    {
        var context = CreateInMemoryContext();
        var controller = new HealthController(context);

        var result = controller.GetHealth();

        Assert.IsType<OkObjectResult>(result);
    }
}
