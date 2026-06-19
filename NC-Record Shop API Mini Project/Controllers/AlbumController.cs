using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NC_Record_Shop_API_Mini_Project.Models;
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
        public ActionResult GetAllAlbums([FromQuery] string? artist = null, [FromQuery] string? genre = null, [FromQuery] int? releaseYear = null, [FromQuery] string? name = null, [FromQuery] int page = 1, [FromQuery] int? pageSize = null, [FromQuery] string? sortBy = null, [FromQuery] string? order = null)
        {
            if (pageSize != null)
            {
                if (page < 1) page = 1;
                var size = pageSize.Value;
                if (size < 1) size = 10;
                if (size > 100) size = 100;
                return Ok(_albumService.GetPagedAlbums(artist, genre, releaseYear, name, page, size, sortBy, order));
            }
            if (artist == null && genre == null && releaseYear == null && name == null && sortBy == null)
            {
                return Ok(_albumService.GetAllAlbums());
            }
            return Ok(_albumService.GetFilteredAlbums(artist, genre, releaseYear, name, sortBy, order));
        }
        [HttpGet("{id}")]
        public ActionResult GetAlbumById(int id)
        {
            var result = _albumService.GetAlbumById(id);
            if (result == null) return NotFound($"No album found with id {id}.");
            return Ok(result);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddAlbum(Album album)
        {
            var result = _albumService.AddAlbum(album);
            return CreatedAtAction(nameof(GetAlbumById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateAlbum(int id, Album album)
        {
            var result = _albumService.UpdateAlbum(id, album);
            if (result == null) return NotFound($"No album found with id {id}.");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteAlbum(int id)
        {
            var result = _albumService.DeleteAlbum(id);
            if (!result) return NotFound($"No album found with id {id}.");
            return NoContent();
        }


    }
}
