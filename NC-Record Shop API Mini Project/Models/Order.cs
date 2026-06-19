using System.ComponentModel.DataAnnotations.Schema;

namespace NC_Record_Shop_API_Mini_Project.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public List<OrderItem> Items { get; set; } = new();

        [NotMapped]
        public decimal Total => Items.Sum(i => i.UnitPrice * i.Quantity);
    }
}
