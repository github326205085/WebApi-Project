using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository: IOrderRepository
    {
        private readonly _326223617BookStoreContext bookStoreContext;
        public OrderRepository(_326223617BookStoreContext bookStoreContext)
        {
            this.bookStoreContext = bookStoreContext;
        }

        public async Task<Order> AddOrder(Order order)
        {
            await bookStoreContext.Order.AddAsync(order);
            await bookStoreContext.SaveChangesAsync();
            return order;
        }

    }
}
