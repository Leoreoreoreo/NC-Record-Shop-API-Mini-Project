namespace NC_Record_Shop_API_Mini_Project.Models
{
    public class Album
    {
        public int Id {get; set;}
        public required string Name {get; set;}
        public required string Artist {get; set;}
        public required string Genre {get; set;}
        public int ReleaseYear {get; set;}
        public int Stock {get; set;}
    }
}