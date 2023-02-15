using OnlineMenu.Persistence;
using OnlineMenu.Responses;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Interfaces
{
    public interface IUserRepository
    {
        Task<ApiResponse> CreateUserAsync(RegisterViewModel model);
        Task<ApiResponse> SigninUserAsync(LoginViewModel model);
        Task<string> GetUserIdAsync(string userName);
        Task<(string userId, string fullName, string UserName, string email, IList<string> roles, int tenantid)> GetUserDetailsAsync(string userId);
        Task<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsByUserNameAsync(string userName);
        Task<string> GetUserNameAsync(string userId);
        Task<ApiResponse> DeleteUserAsync(string userId);
        Task<bool> IsUniqueUserName(string userName);
        Task<List<ApplicationUser>> GetAllUsersAsync();
        Task<List<(string id, string userName, string email, IList<string> roles)>> GetAllUsersDetailsAsync();
        // Role Section
        Task<ApiResponse> CreateRoleAsync(string roleName);
        Task<ApiResponse> DeleteRoleAsync(string roleId);
        Task<List<(string id, string roleName)>> GetRolesAsync();
        Task<(string id, string roleName)> GetRoleByIdAsync(string id);
        Task<ApiResponse> UpdateRole(string id, string roleName);

        // User's Role section
        Task<List<string>> GetUserRolesAsync(string userId);
        Task<ApiResponse> AssignUserToRole(string userName, IList<string> roles);
        Task<ApiResponse> UpdateUsersRole(string userName, IList<string> usersRole);
        Task<ApiResponse> ChangePasswordAsync(ChangePasswordViewModel model);
        Task<ApiResponse> ForgetPasswordAsync(string email);
        Task<ApiResponse> ResetPasswordAsync(ResetPasswordViewModel model);
    }
}
