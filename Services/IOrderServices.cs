using Entities;

namespace Services
{
    public interface IOrderServices
    {
        Task<Order> AddOrders(Order order);
    }
}