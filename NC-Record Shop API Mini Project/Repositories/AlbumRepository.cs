using NC_Record_Shop_API_Mini_Project.Models;
using NC_Record_Shop_API_Mini_Project.Data;
namespace NC_Record_Shop_API_Mini_Project.Repositories
{
    public interface IAlbumRepository
    {
        List<Album> GetAllAlbums();
        List<Album> GetFilteredAlbums(string? artist, string? genre, int? releaseYear, string? name);
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
        public List<Album> GetFilteredAlbums(string? artist, string? genre, int? releaseYear, string? name)
        {
            var query = _appDbContext.Albums.AsQueryable();
            if (!string.IsNullOrWhiteSpace(artist))
                query = query.Where(a => a.Artist.ToLower().Contains(artist.ToLower()));
            if (!string.IsNullOrWhiteSpace(genre))
                query = query.Where(a => a.Genre.ToLower() == genre.ToLower());
            if (releaseYear.HasValue)
                query = query.Where(a => a.ReleaseYear == releaseYear.Value);
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(a => a.Name.ToLower().Contains(name.ToLower()));
            return query.ToList();
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