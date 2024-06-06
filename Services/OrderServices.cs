using Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Repository;


namespace Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository orderRepository;
        private readonly _326223617BookStoreContext bookStoreContext;
        private readonly ILogger<OrderServices> logger;
  
  
            
    
        public OrderServices(IOrderRepository orderRepository, _326223617BookStoreContext bookStoreContext, ILogger<OrderServices> logger)
        {
            this.orderRepository = orderRepository; 
            this.bookStoreContext = bookStoreContext;
            this.logger = logger;
        }

        public async Task<Order> AddOrders(Order order)
        {
            double sum = 0;
            foreach (OrderItem prod in order.OrderItems)
            {
                Product product= await bookStoreContext.Products.FindAsync(prod.ProductId);
                sum =sum+ ((double)product.Price * (double)prod.Quantity);
            }
            if (sum == order.OrderSum)
                return await orderRepository.AddOrder(order);
            else
                 logger.LogError($"the order sum and the real price arn't the match, {order.OrderSum} and the real sum Order is {sum}");
            return null;

        }

    }
}
