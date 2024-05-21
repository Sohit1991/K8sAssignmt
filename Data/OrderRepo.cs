using Microsoft.EntityFrameworkCore;
using ProductOrdering.Entity;

namespace ProductOrdering.Data
{
    public class OrderRepo : IOrderRepo
    {
        private readonly OrderContext _orderContext;

        public OrderRepo(OrderContext orderContext)
        {
            this._orderContext = orderContext;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await _orderContext.Orders.ToListAsync();
        }

        public async Task<Order> GetOrder(int id)
        {
            return await _orderContext.Orders.FindAsync(id);

        }
        public async Task<Order> CreateOrder(Order order)
        {
            await _orderContext.Orders.AddAsync(order);
            await _orderContext.SaveChangesAsync();
            return order;
        }

        public async Task DeleteOrder(int id)
        {
            var orderDetail = await _orderContext.Orders.FindAsync(id);
            _orderContext.Remove(orderDetail);
            await _orderContext.SaveChangesAsync();
        }


    }
}
