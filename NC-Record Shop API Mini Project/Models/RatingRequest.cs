using System.ComponentModel.DataAnnotations;

namespace NC_Record_Shop_API_Mini_Project.Models
{
    public class RatingRequest
    {
        [Range(1, 5)]
        public int Stars { get; set; }
    }
}
