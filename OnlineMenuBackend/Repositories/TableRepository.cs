using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using OnlineMenu.Interfaces;
using OnlineMenu.Models;
using OnlineMenu.Responses;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Repositories
{
    public class TableRepository : ITableRepository
    {

        private OMContext context;
        private IValidator<TableViewModel> _validator;

        public TableRepository(OMContext context, IValidator<TableViewModel> validator)
        {
            this.context = context;
            _validator = validator;
        }

        public async Task<ApiResponse> DeleteTable(Table table)
        {
            if (table == null)
            {
                return new ApiResponse(400, "This table doesn't exist");
            }

            var result = context.Tables.Remove(table);
            await context.SaveChangesAsync();
            return new ApiResponse(200, "Table deleted");
        }

        public async Task<List<Table>> GetAllTables()
        {
            var result = await context.Tables.ToListAsync();
            return result;
        }

        public async Task<Table> GetTableById(int id)
        {
            var result = await context.Tables.FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<ApiResponse> AddTable(TableViewModel table)
        {
            ValidationResult result = await _validator.ValidateAsync(table);

            if (!result.IsValid)
            {
                return new ApiResponse(400, result.ToString());
            }
            var model = new Table
            {
                Number = table.Number,
                TenantId = table.TenantId
            };

            await context.Tables.AddAsync(model);
            await context.SaveChangesAsync();
            return new ApiResponse(200, "Table added");
        }

        public async Task<ApiResponse> UpdateTable(Table table)
        {
            if (table == null)
            {
                return new ApiResponse(400, "Table doesn't exist");
            }
            context.Update(table);
            await context.SaveChangesAsync();
            return new ApiResponse(200, "Table added");
        }
    }
}

