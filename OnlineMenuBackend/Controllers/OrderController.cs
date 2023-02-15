using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMenu.Interfaces;
using OnlineMenu.Models;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;

        public OrderController(IOrderRepository _repository)
        {
            repository = _repository;
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "superadmin")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "waiter")]
        [HttpGet("getallorders")]
        public async Task<IActionResult> GetAllOrders(int id)
        {
            var prod = await repository.GetAllOrdersFromTenant(id);
            return Ok(prod);
        }

        [HttpPost("addorder")]
        public async Task<IActionResult> AddOrder(OrderViewModel order)
        {
            var result = await repository.AddOrder(order);
            return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
        }
       
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "superadmin")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "waiter")]
        [HttpPost("deleteorder")]
        public async Task<IActionResult> DeleteOrder(Order order)
        {
            await repository.DeleteOrder(order);
            return Ok();
        }

        [HttpGet("getorderbyid")]
        public async Task <IActionResult> GetOrderById(int id)
        {
            await repository.GetOrderById(id);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "superadmin")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "waiter")]
        [HttpPost("updateorder")]
        public async Task<IActionResult> UpdateOrder(Order order)
        {
            await repository.DeleteOrder(order);
            return Ok();
        }
    }
}
