using NC_Record_Shop_API_Mini_Project.Repositories;
using NC_Record_Shop_API_Mini_Project.Models;

namespace NC_Record_Shop_API_Mini_Project.Services
{
    public interface IAlbumService
    {
        List<Album> GetAllAlbums();
        Album GetAlbumById(int id);
        Album AddAlbum(Album album);
        Album UpdateAlbum(int id, Album album);
        bool DeleteAlbum(int id);
    }

    public class AlbumService : IAlbumService
    {
        readonly IAlbumRepository _albumRepository;
        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public List<Album> GetAllAlbums() { return null; }
        public Album GetAlbumById(int id) { return null; }
        public Album AddAlbum(Album album) { return null; }
        public Album UpdateAlbum(int id, Album album) { return null; }
        public bool DeleteAlbum(int id) { return false; }
    }
}