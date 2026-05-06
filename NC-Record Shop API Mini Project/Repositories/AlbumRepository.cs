using NC_Record_Shop_API_Mini_Project.Models;
using NC_Record_Shop_API_Mini_Project.Data;
namespace NC_Record_Shop_API_Mini_Project.Repositories
{
    public interface IAlbumRepository
    {
        List<Album> GetAllAlbums();
        Album GetAlbumById(int id);
        Album AddAlbum(Album album);
        Album UpdateAlbum(int id, Album album);
        bool DeleteAlbum(int id);

    }
    public class AlbumRepository : IAlbumRepository
    {
        private readonly AppDbContext _appDbContext;
        public AlbumRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public List<Album> GetAllAlbums() 
        { 
            return _appDbContext.Albums.ToList(); 
        }
        public Album GetAlbumById(int id) 
        { 
            return _appDbContext.Albums.FirstOrDefault(a => a.Id == id); 
        }
        public Album AddAlbum(Album album) { return null; }
        public Album UpdateAlbum(int id, Album album) { return null; }
        public bool DeleteAlbum(int id) { return false; }

    }
}