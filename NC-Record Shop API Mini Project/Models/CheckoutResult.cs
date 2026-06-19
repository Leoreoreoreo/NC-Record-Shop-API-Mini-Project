namespace NC_Record_Shop_API_Mini_Project.Models
{
    public class CheckoutResult
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public Order? Order { get; set; }
    }
}
