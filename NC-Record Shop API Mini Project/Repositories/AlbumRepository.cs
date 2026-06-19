using NC_Record_Shop_API_Mini_Project.Models;
using NC_Record_Shop_API_Mini_Project.Data;
namespace NC_Record_Shop_API_Mini_Project.Repositories
{
    public interface IAlbumRepository
    {
        List<Album> GetAllAlbums();
        List<Album> GetFilteredAlbums(string? artist, string? genre, int? releaseYear, string? name, string? sortBy, string? order);
        PagedAlbums GetPagedAlbums(string? artist, string? genre, int? releaseYear, string? name, int page, int pageSize, string? sortBy, string? order);
        Album? GetAlbumById(int id);
        Album AddAlbum(Album album);
        Album? UpdateAlbum(int id, Album album);
        bool DeleteAlbum(int id);

    }
    public class AlbumRepository : IAlbumRepository
    {
        private readonly RecordShopDbContext _appDbContext;
        public AlbumRepository(RecordShopDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public List<Album> GetAllAlbums()
        {
            return _appDbContext.Albums.ToList();
        }
        public List<Album> GetFilteredAlbums(string? artist, string? genre, int? releaseYear, string? name, string? sortBy, string? order)
        {
            var query = ApplyFilters(_appDbContext.Albums.AsQueryable(), artist, genre, releaseYear, name);
            return ApplySort(query, sortBy, order).ToList();
        }
        public PagedAlbums GetPagedAlbums(string? artist, string? genre, int? releaseYear, string? name, int page, int pageSize, string? sortBy, string? order)
        {
            var query = ApplyFilters(_appDbContext.Albums.AsQueryable(), artist, genre, releaseYear, name);
            var totalCount = query.Count();
            query = ApplySort(query, sortBy, order);
            var albums = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return new PagedAlbums
            {
                Albums = albums,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }
        private static IQueryable<Album> ApplyFilters(IQueryable<Album> query, string? artist, string? genre, int? releaseYear, string? name)
        {
            if (!string.IsNullOrWhiteSpace(artist))
                query = query.Where(a => a.Artist.ToLower().Contains(artist.ToLower()));
            if (!string.IsNullOrWhiteSpace(genre))
                query = query.Where(a => a.Genre.ToLower() == genre.ToLower());
            if (releaseYear.HasValue)
                query = query.Where(a => a.ReleaseYear == releaseYear.Value);
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(a => a.Name.ToLower().Contains(name.ToLower()));
            return query;
        }
        private IQueryable<Album> ApplySort(IQueryable<Album> query, string? sortBy, string? order)
        {
            var descending = string.Equals(order, "desc", StringComparison.OrdinalIgnoreCase);
            switch (sortBy?.ToLower())
            {
                case "name":
                    return descending ? query.OrderByDescending(a => a.Name) : query.OrderBy(a => a.Name);
                case "artist":
                    return descending ? query.OrderByDescending(a => a.Artist) : query.OrderBy(a => a.Artist);
                case "releaseyear":
                    return descending ? query.OrderByDescending(a => a.ReleaseYear) : query.OrderBy(a => a.ReleaseYear);
                case "price":
                    return descending ? query.OrderByDescending(a => a.Price) : query.OrderBy(a => a.Price);
                case "rating":
                    return descending
                        ? query.OrderByDescending(a => _appDbContext.Ratings.Where(r => r.AlbumId == a.Id).Average(r => (double?)r.Stars) ?? 0.0)
                        : query.OrderBy(a => _appDbContext.Ratings.Where(r => r.AlbumId == a.Id).Average(r => (double?)r.Stars) ?? 0.0);
                default:
                    return query;
            }
        }
        public Album? GetAlbumById(int id)
        {
            return _appDbContext.Albums.FirstOrDefault(a => a.Id == id);
        }
        public Album AddAlbum(Album album)
        {
            _appDbContext.Albums.Add(album);
            _appDbContext.SaveChanges();
            return album;
        }
        public Album? UpdateAlbum(int id, Album album)
        {
            var existing = _appDbContext.Albums.FirstOrDefault(a => a.Id == id);
            if (existing == null) return null;
            existing.Name = album.Name;
            existing.Artist = album.Artist;
            existing.Genre = album.Genre;
            existing.ReleaseYear = album.ReleaseYear;
            existing.Stock = album.Stock;
            existing.Price = album.Price;
            _appDbContext.SaveChanges();
            return existing;
        }
        public bool DeleteAlbum(int id)
        {
            var existing = _appDbContext.Albums.FirstOrDefault(a => a.Id == id);
            if (existing == null) return false;
            _appDbContext.Albums.Remove(existing);
            _appDbContext.SaveChanges();
            return true;
        }

    }
}