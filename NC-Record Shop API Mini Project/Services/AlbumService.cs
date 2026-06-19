using NC_Record_Shop_API_Mini_Project.Repositories;
using NC_Record_Shop_API_Mini_Project.Models;


namespace NC_Record_Shop_API_Mini_Project.Services
{
    public interface IAlbumService
    {
        List<Album> GetAllAlbums();
        List<Album> GetFilteredAlbums(string? artist, string? genre, int? releaseYear, string? name, string? sortBy, string? order);
        PagedAlbums GetPagedAlbums(string? artist, string? genre, int? releaseYear, string? name, int page, int pageSize, string? sortBy, string? order);
        Album? GetAlbumById(int id);
        Album AddAlbum(Album album);
        Album? UpdateAlbum(int id, Album album);
        bool DeleteAlbum(int id);
    }

    public class AlbumService : IAlbumService
    {
        readonly IAlbumRepository _albumRepository;
        public AlbumService(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public List<Album> GetAllAlbums()
        {
            return _albumRepository.GetAllAlbums();

        }
        public List<Album> GetFilteredAlbums(string? artist, string? genre, int? releaseYear, string? name, string? sortBy, string? order)
        {
            return _albumRepository.GetFilteredAlbums(artist, genre, releaseYear, name, sortBy, order);
        }
        public PagedAlbums GetPagedAlbums(string? artist, string? genre, int? releaseYear, string? name, int page, int pageSize, string? sortBy, string? order)
        {
            return _albumRepository.GetPagedAlbums(artist, genre, releaseYear, name, page, pageSize, sortBy, order);
        }
        public Album? GetAlbumById(int id)
        {
            return _albumRepository.GetAlbumById(id);
        }
        public Album AddAlbum(Album album)
        {
            return _albumRepository.AddAlbum(album);
        }
        public Album? UpdateAlbum(int id, Album album)
        {
            return _albumRepository.UpdateAlbum(id, album);
        }
        public bool DeleteAlbum(int id)
        {
            return _albumRepository.DeleteAlbum(id);
        }
    }
}