using OnlineMenu.Models;
using OnlineMenu.Responses;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Interfaces
{
    public interface IOrderRepository
    {
        Task<ApiResponse> AddOrder(OrderViewModel order);
        Task<IEnumerable<Order>> GetAllOrdersFromTenant(int id);
        Task<ApiResponse> DeleteOrder(Order order);
        Task<Order> GetOrderById(int id);
        Task<ApiResponse> UpdateOrder(Order order);
    }
}
