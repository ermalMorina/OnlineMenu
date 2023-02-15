using OnlineMenu.Models;
using OnlineMenu.Responses;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Interfaces
{
    public interface ITableRepository
    {
        Task<ApiResponse> AddTable(TableViewModel table);
        Task<List<Table>> GetAllTables();
        Task<ApiResponse> DeleteTable(Table table);
        Task<Table> GetTableById(int id);
        Task<ApiResponse> UpdateTable(Table table);
    }
}
