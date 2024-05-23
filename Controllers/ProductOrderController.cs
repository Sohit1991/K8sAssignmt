using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using ProductOrdering.Data;
using ProductOrdering.Entity;
using System.Text;

namespace ProductOrdering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductOrderController : ControllerBase
    {
        private readonly IOrderRepo _orderRepo;

        public ProductOrderController(IOrderRepo orderRepo)
        {
            this._orderRepo = orderRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            var orders = await _orderRepo.GetAllOrders();
            if (orders == null)
                return NotFound();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _orderRepo.GetOrder(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order orderRequest)
        {
            var order = await _orderRepo.CreateOrder(orderRequest);
            if (order == null)
                return NotFound();
            return CreatedAtAction(nameof(GetOrder), new { id = orderRequest.Id }, order);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            await _orderRepo.DeleteOrder(id);
            return NoContent();
        }
        //[HttpGet]
        //public string Test()
        //{
        //    return "success";
        //}
    }
}
