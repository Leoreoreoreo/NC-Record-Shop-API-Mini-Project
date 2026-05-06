using Microsoft.AspNetCore.Mvc;
using NC_Record_Shop_API_Mini_Project.Services;

namespace NC_Record_Shop_API_Mini_Project.Controllers
{
    [ApiController]
    [Route("api/albums")]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public ActionResult GetAllAlbums()
        {
            var result = _albumService.GetAllAlbums();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public ActionResult GetAlbumById(int id)
        {
            var result = _albumService.GetAlbumById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

    }
}
