using Microsoft.EntityFrameworkCore;
using NC_Record_Shop_API_Mini_Project.Data;
using NC_Record_Shop_API_Mini_Project.Models;

namespace NC_Record_Shop_API_Mini_Project.Repositories
{
    public interface IOrderRepository
    {
        CheckoutResult Checkout(string userId, List<OrderItemRequest> items);
        List<Order> GetOrdersForUser(string userId);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly RecordShopDbContext _context;

        public OrderRepository(RecordShopDbContext context)
        {
            _context = context;
        }

        public CheckoutResult Checkout(string userId, List<OrderItemRequest> items)
        {
            var order = new Order { UserId = userId, CreatedAt = DateTime.UtcNow };

            foreach (var item in items)
            {
                var album = _context.Albums.FirstOrDefault(a => a.Id == item.AlbumId);
                if (album == null)
                    return new CheckoutResult { Success = false, Error = $"No album found with id {item.AlbumId}." };
                if (item.Quantity < 1)
                    return new CheckoutResult { Success = false, Error = $"Quantity for album {item.AlbumId} must be at least 1." };
                if (album.Stock < item.Quantity)
                    return new CheckoutResult { Success = false, Error = $"Not enough stock for '{album.Name}' (requested {item.Quantity}, {album.Stock} in stock)." };

                album.Stock -= item.Quantity;
                order.Items.Add(new OrderItem
                {
                    AlbumId = album.Id,
                    AlbumName = album.Name,
                    Quantity = item.Quantity,
                    UnitPrice = album.Price
                });
            }

            _context.Orders.Add(order);
            _context.SaveChanges();
            return new CheckoutResult { Success = true, Order = order };
        }

        public List<Order> GetOrdersForUser(string userId)
        {
            return _context.Orders
                .Include(o => o.Items)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToList();
        }
    }
}
