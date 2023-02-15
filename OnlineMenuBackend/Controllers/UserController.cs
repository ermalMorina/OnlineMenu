using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineMenu.Interfaces;
using OnlineMenu.Viewmodels;

namespace OnlineMenu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IUserRepository repository;

        public UserController(IUserRepository _repository)
        {
            repository = _repository;
        }

        [HttpPost("createuser")]
        public async Task<IActionResult> CreateUser(RegisterViewModel model)
        {
            var result = await repository.CreateUserAsync(model);
            return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
        }

        [HttpPut("createrole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var result = await repository.CreateRoleAsync(roleName);
            return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
        }

        [HttpPost("assignusertorole")]
        public async Task<IActionResult> AssignUserToRole(string userName, IList<string> roles)
        {
            var result = await repository.AssignUserToRole(userName, roles);
            return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("deleterole")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var result = await repository.DeleteRoleAsync(roleId);
            return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("deleteuser")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await repository.DeleteUserAsync(userId);
            return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getallusers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await repository.GetAllUsersAsync();
            return result != null ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getuserid")]
        public async Task<IActionResult> GetUserId(string username)
        {
            var result = await repository.GetUserIdAsync(username);
            return result !=null ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getusername")]
        public async Task<IActionResult> GetUsername(string userId)
        {
            var result = await repository.GetUserNameAsync(userId);
            return result != null ? Ok(result) : BadRequest(result);

        }

        [HttpPost("isuniqueusername")]
        public async Task<IActionResult> IsUniqueUserName(string username)
        {
            var result = await repository.IsUniqueUserName(username);
            return Ok(result);
        }

        [HttpPost("signinuser")]
        public async Task<IActionResult> SigninUser(LoginViewModel model)
        {
            var result = await repository.SigninUserAsync(model);
            return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
        }

        [HttpGet("getrolebyid")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var user = User.Identity.Name;
            var result = await repository.GetRoleByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("updaterole")]
        public async Task<IActionResult> UpdateRole(string id, string roleName)
        {
            var result = await repository.UpdateRole(id, roleName);
            return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
        }

        [HttpPost("updateusersrole")]
        public async Task<IActionResult> UpdateUsersRole(string userName, IList<string> usersRole)
        {
            var result = await repository.UpdateUsersRole(userName, usersRole);
            return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
        }

        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var result = await repository.ChangePasswordAsync(model);
            return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
       
        }
        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(string email)
        {
            var result = await repository.ForgetPasswordAsync(email);
            return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
        }
        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var result = await repository.ResetPasswordAsync(model);
            return result.StatusCode == 200 ? Ok(result) : BadRequest(result);
        }

    }
}
 