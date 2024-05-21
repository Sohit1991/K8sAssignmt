using ProductOrdering.Entity;

namespace ProductOrdering.Data
{
    public interface IOrderRepo
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrder(int id);
        Task<Order> CreateOrder(Order order);
        Task DeleteOrder(int id);
    }
}
