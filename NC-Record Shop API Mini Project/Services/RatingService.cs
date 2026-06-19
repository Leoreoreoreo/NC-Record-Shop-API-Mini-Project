using NC_Record_Shop_API_Mini_Project.Models;
using NC_Record_Shop_API_Mini_Project.Repositories;

namespace NC_Record_Shop_API_Mini_Project.Services
{
    public interface IRatingService
    {
        bool AlbumExists(int albumId);
        Rating? AddRating(int albumId, int stars);
        RatingSummary GetSummary(int albumId);
    }

    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        public bool AlbumExists(int albumId)
        {
            return _ratingRepository.AlbumExists(albumId);
        }
        public Rating? AddRating(int albumId, int stars)
        {
            return _ratingRepository.AddRating(albumId, stars);
        }
        public RatingSummary GetSummary(int albumId)
        {
            return _ratingRepository.GetSummary(albumId);
        }
    }
}
