using Microsoft.AspNetCore.Mvc;
using NC_Record_Shop_API_Mini_Project.Models;
using NC_Record_Shop_API_Mini_Project.Services;

namespace NC_Record_Shop_API_Mini_Project.Controllers
{
    [ApiController]
    [Route("api/albums/{albumId}/ratings")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpGet]
        public ActionResult GetRatings(int albumId)
        {
            if (!_ratingService.AlbumExists(albumId)) return NotFound($"No album found with id {albumId}.");
            return Ok(_ratingService.GetSummary(albumId));
        }

        [HttpPost]
        public ActionResult AddRating(int albumId, RatingRequest request)
        {
            var rating = _ratingService.AddRating(albumId, request.Stars);
            if (rating == null) return NotFound($"No album found with id {albumId}.");
            return CreatedAtAction(nameof(GetRatings), new { albumId }, rating);
        }
    }
}
