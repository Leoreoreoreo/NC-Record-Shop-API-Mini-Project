using NC_Record_Shop_API_Mini_Project.Models;
using NC_Record_Shop_API_Mini_Project.Repositories;

namespace NC_Record_Shop_API_Mini_Project.Services
{
    public interface IOrderService
    {
        CheckoutResult Checkout(string userId, List<OrderItemRequest> items);
        List<Order> GetOrdersForUser(string userId);
    }

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public CheckoutResult Checkout(string userId, List<OrderItemRequest> items)
        {
            return _orderRepository.Checkout(userId, items);
        }

        public List<Order> GetOrdersForUser(string userId)
        {
            return _orderRepository.GetOrdersForUser(userId);
        }
    }
}
