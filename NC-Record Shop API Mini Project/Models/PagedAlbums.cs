namespace NC_Record_Shop_API_Mini_Project.Models
{
    public class PagedAlbums
    {
        public List<Album> Albums { get; set; } = new();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
