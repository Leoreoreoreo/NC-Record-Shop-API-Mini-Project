using Microsoft.AspNetCore.Mvc;
using NC_Record_Shop_API_Mini_Project.Data;

namespace NC_Record_Shop_API_Mini_Project.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public HealthController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public ActionResult GetHealth()
        {
            var canConnectToDatabase = _appDbContext.Database.CanConnect();
            var status = new
            {
                Api = "Healthy",
                Database = canConnectToDatabase ? "Healthy" : "Unhealthy"
            };

            if (!canConnectToDatabase) return StatusCode(503, status);
            return Ok(status);
        }
    }
}
