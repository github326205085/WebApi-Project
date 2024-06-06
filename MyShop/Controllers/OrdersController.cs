using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Services;

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly _326223617BookStoreContext bookStoreContext;

        private readonly IOrderServices OrderServices;
        private readonly IMapper mapper;

        public OrdersController(IOrderServices OrderServices, IMapper mapper, _326223617BookStoreContext bookStoreContext)
        {
            this.OrderServices = OrderServices;
            this.mapper = mapper;
            this.bookStoreContext = bookStoreContext;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Post([FromBody] OrderGetDTO orderDTO)
        {

            var userExists = await bookStoreContext.Users.AnyAsync(u => u.UserId == orderDTO.UserId);

            if (!userExists)
            {
                return BadRequest("Invalid UserId. User does not exist.");
            }
            //List<OrderItem> orderItems = mapper.Map<List<OrderItemDTO>, List<OrderItem>>((List<OrderItemDTO>)orderDTO.orderItems);

            Order order = mapper.Map<OrderGetDTO, Order>(orderDTO);
           // order.OrderItems = orderItems;
            Order orderNew = await OrderServices.AddOrders(order);

            OrderReturnDTO orderNewDTO = mapper.Map<Order, OrderReturnDTO>(orderNew);

            if (orderNewDTO != null)
            {
                return CreatedAtAction(nameof(Post), new { id = orderNewDTO.OrderId }, orderNewDTO);
            }

            return NoContent();
        }
    
    }
}
