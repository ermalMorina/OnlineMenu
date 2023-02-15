using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMenu.Interfaces;
using OnlineMenu.Models;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Controllers
{
    public class TableController : Controller
    {
        private ITableRepository repository;

        public TableController(ITableRepository _repository)
        {
            repository = _repository;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "superadmin")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "waiter")]
        [HttpGet("getalltables")]
        public async Task<IActionResult> GetAllTables()
        {
            var prod = await repository.GetAllTables();
            return Ok(prod);
        }

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "superadmin")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "waiter")]
        [HttpPost("addtable")]
        public async Task<IActionResult> AddTable(TableViewModel table)
        {
            var result = await repository.AddTable(table);
            return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "superadmin")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "waiter")]
        [HttpPost("deletetable")]
        public async Task<IActionResult> DeleteTable(Table table)
        {
            await repository.DeleteTable(table);
            return Ok();
        }

        [HttpGet("gettablebyid")]
        public async Task<IActionResult> GetTableById(int id)
        {
            await repository.GetTableById(id);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "admin")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "superadmin")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "waiter")]
        [HttpPost("updatetable")]
        public async Task<IActionResult> UpdateTable(Table table)
        {
            await repository.UpdateTable(table);
            return Ok();
        }
    }
}
