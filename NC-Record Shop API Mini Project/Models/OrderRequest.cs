using System.ComponentModel.DataAnnotations;

namespace NC_Record_Shop_API_Mini_Project.Models
{
    public class OrderRequest
    {
        [Required]
        [MinLength(1)]
        public List<OrderItemRequest> Items { get; set; } = new();
    }

    public class OrderItemRequest
    {
        public int AlbumId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
