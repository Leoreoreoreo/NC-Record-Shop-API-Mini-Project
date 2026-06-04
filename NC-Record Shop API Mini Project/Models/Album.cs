using System.ComponentModel.DataAnnotations;

namespace NC_Record_Shop_API_Mini_Project.Models
{
    public class Album
    {
        public int Id {get; set;}

        [Required]
        public required string Name {get; set;}

        [Required]
        public required string Artist {get; set;}

        [Required]
        public required string Genre {get; set;}

        [Range(1900, 2100)]
        public int ReleaseYear {get; set;}

        [Range(0, int.MaxValue)]
        public int Stock {get; set;}

        [Range(0.01, 99999.99)]
        public decimal Price {get; set;}
    }
}
