using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using OnlineMenu.Interfaces;
using OnlineMenu.Models;
using OnlineMenu.Responses;
using OnlineMenu.SignalR;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private OMContext context;
        private IValidator<OrderViewModel> _validator;
        private TenantAdminDbContext _tenantAdminDbContext;
        private IHubContext<GroupHub> _hubContext;

        public OrderRepository(OMContext context, IValidator<OrderViewModel> validator, TenantAdminDbContext tenantAdminDbContext, IHubContext<GroupHub> hubContext)
        {
            this.context = context;
            _validator = validator;
            _tenantAdminDbContext = tenantAdminDbContext;
            _hubContext = hubContext;
        }

        public async Task<ApiResponse> DeleteOrder(Order order)
        {
            if (order == null)
            {
                return new ApiResponse(400, "This order doesn't exist");
            }

            var result = context.Orders.Remove(order);
            await context.SaveChangesAsync();
            return new ApiResponse(200, "Order deleted");
        }

        public async Task<IEnumerable<Order>> GetAllOrdersFromTenant(int id)
        {
            var result = await context.Orders.Where(x => x.TenantId == id).ToListAsync();
            return result;
        }

        public async Task<Order> GetOrderById(int id)
        {
            var result = await context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<ApiResponse> AddOrder(OrderViewModel order)
        {
            ValidationResult result = await _validator.ValidateAsync(order);

            if (!result.IsValid)
            {
                return new ApiResponse(400, result.ToString());
            }

            //foreach (var productOrder in order.Products)
            //{
            //    var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productOrder.ProductId);
            //    order.Price += product.Price * productOrder.Quantity;
            //}

            var model = new Order
            {
                TenantId = order.TenantId,
                Note = order.Note,
                isLocal = order.isLocal,
                Address = order.Address,
                Telephone = order.Telephone,
                isClosed = order.isClosed,
                TableId = order.TableId,
                DateTime = DateTime.Now,
                ProductOrders = order.Products,
                Price = order.Price,
            };

            await context.Orders.AddAsync(model);
            await _hubContext.Clients.Group(model.TenantId.ToString()).SendAsync("RecieveOrder", order);
            await context.SaveChangesAsync();

            return new ApiResponse(200, "Order added");
        }

        public async Task<ApiResponse> UpdateOrder(Order order)
        {
            if (order == null)
            {
                return new ApiResponse(400, "Order doesn't exist");
            }
            context.Update(order);
            await context.SaveChangesAsync();
            return new ApiResponse(200, "Order added");
        }
    }
}
