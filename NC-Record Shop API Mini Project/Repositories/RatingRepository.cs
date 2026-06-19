using NC_Record_Shop_API_Mini_Project.Data;
using NC_Record_Shop_API_Mini_Project.Models;

namespace NC_Record_Shop_API_Mini_Project.Repositories
{
    public interface IRatingRepository
    {
        bool AlbumExists(int albumId);
        Rating? AddRating(int albumId, int stars);
        RatingSummary GetSummary(int albumId);
    }

    public class RatingRepository : IRatingRepository
    {
        private readonly RecordShopDbContext _context;
        public RatingRepository(RecordShopDbContext context)
        {
            _context = context;
        }

        public bool AlbumExists(int albumId)
        {
            return _context.Albums.Any(a => a.Id == albumId);
        }

        public Rating? AddRating(int albumId, int stars)
        {
            if (!AlbumExists(albumId)) return null;
            var rating = new Rating { AlbumId = albumId, Stars = stars };
            _context.Ratings.Add(rating);
            _context.SaveChanges();
            return rating;
        }

        public RatingSummary GetSummary(int albumId)
        {
            var ratings = _context.Ratings.Where(r => r.AlbumId == albumId).ToList();
            return new RatingSummary
            {
                AlbumId = albumId,
                Count = ratings.Count,
                AverageStars = ratings.Count == 0 ? 0 : Math.Round(ratings.Average(r => r.Stars), 2)
            };
        }
    }
}
