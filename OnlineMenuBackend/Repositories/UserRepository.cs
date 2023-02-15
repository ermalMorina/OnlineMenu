using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using OnlineMenu.Interfaces;
using OnlineMenu.Persistence;
using OnlineMenu.Responses;
using OnlineMenu.SignalR;
using OnlineMenu.Viewmodels;
using System.Text;

namespace OnlineMenu.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IValidator<LoginViewModel> _validator;
        private IValidator<RegisterViewModel> _registervalidator;
        private OMContext _omContext;
        private TenantAdminDbContext _tenantAdminDbContext;
        private IHubContext<GroupHub> _groupHub;
        private ITokenGenerator _tokenGenerator;
        private IEmailRepository _emailRepository;
        private readonly IConfiguration _configuration;
        public ModelStateDictionary ModelState { get; private set; }
        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IValidator<LoginViewModel> validator, OMContext omContext, TenantAdminDbContext context, IHubContext<GroupHub> groupHub, IValidator<RegisterViewModel> registervalidator, ITokenGenerator tokenGenerator, IEmailRepository emailRepository, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _roleManager = roleManager;
            _validator = validator;
            _omContext = omContext;
            _tenantAdminDbContext = context;
            _groupHub = groupHub;
            _registervalidator= registervalidator;
            _tokenGenerator= tokenGenerator;
            _emailRepository= emailRepository;
            _configuration= configuration;

        }

        //DUHET ME I VALIDU
        public async Task<ApiResponse> AssignUserToRole(string userName, IList<string> roles)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                return new ApiResponse(404, "User not found");
            }

            var result = await _userManager.AddToRolesAsync(user, roles);
            return new ApiOkResponse(200);
        }

        public async Task<ApiResponse> CreateRoleAsync(string roleName)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (!result.Succeeded)
            {
                return new ApiResponse(404, result.Errors.ToString());
            }
            return new ApiOkResponse(200);
        }


        public async Task<ApiResponse> CreateUserAsync(RegisterViewModel model)
        {
            ValidationResult results= await _registervalidator.ValidateAsync(model);
            
            if (!results.IsValid)
            {
                return new ApiResponse(400, results.ToString());
            }

            var user = new ApplicationUser()
            {
                FullName = model.FullName,
                UserName = model.Username,
                Email = model.Email,
                TenantId= model.TenantId,
                PasswordHash= model.Password,
                PhoneNumber= model.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, user.PasswordHash);

            if (!result.Succeeded)
            {
                return new ApiResponse(404, "Something went wrong");
            }

            var addUserRole = await _userManager.AddToRolesAsync(user, model.Roles);
            if (!addUserRole.Succeeded)
            {
                return new ApiResponse(404, "Something went wrong");
            }
            return new ApiOkResponse(200);
        }

        public async Task<ApiResponse> DeleteRoleAsync(string roleId)
        {
            var roleDetails = await _roleManager.FindByIdAsync(roleId);
            if (roleDetails == null)
            {
                return new ApiResponse(400, "Role not found");
            }

            if (roleDetails.Name == "Administrator")
            {
                return new ApiResponse(400, "You can not delete Administrator Role");
            }
            var result = await _roleManager.DeleteAsync(roleDetails);
            if (!result.Succeeded)
            {
                return new ApiResponse(400, result.Errors.ToString());
            }
            return new ApiResponse(200);
        }

        public async Task<ApiResponse> DeleteUserAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return new ApiResponse(400, "User not found");
                //throw new Exception("User not found");
            }

            if (user.UserName == "system" || user.UserName == "admin")
            {
                return new ApiResponse(400, "You can not delete system or admin user");
                //throw new BadRequestException("You can not delete system or admin user");
            }
            var result = await _userManager.DeleteAsync(user);
            return new ApiOkResponse(200);
        }

        public async Task<List<ApplicationUser>> GetAllUsersAsync()
        {
            var result = await _userManager.Users.ToListAsync();
            return result;
        }

        public Task<List<(string id, string userName, string email, IList<string> roles)>> GetAllUsersDetailsAsync()
        {
            throw new NotImplementedException();

            //var roles = await _userManager.GetRolesAsync(user);
            //return (user.Id, user.UserName, user.Email, roles);

            //var users = _userManager.Users.ToListAsync();
        }

        public async Task<List<(string id, string roleName)>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.Select(x => new
            {
                x.Id,
                x.Name
            }).ToListAsync();

            return roles.Select(role => (role.Id, role.Name)).ToList();
        }

        public async Task<(string userId, string fullName, string UserName, string email, IList<string> roles, int tenantid)> GetUserDetailsAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var roles = await _userManager.GetRolesAsync(user);
            return (user.Id, user.FullName, user.UserName, user.Email, roles,user.TenantId);
        }

        public async Task<(string userId, string fullName, string UserName, string email, IList<string> roles)> GetUserDetailsByUserNameAsync(string userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var roles = await _userManager.GetRolesAsync(user);
            return (user.Id, user.FullName, user.UserName, user.Email, roles);
        }

        public async Task<string> GetUserIdAsync(string userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
            {
                throw new Exception("User not found");
                //throw new Exception("User not found");
            }
            var userid=  await _userManager.GetUserIdAsync(user);
            return userid;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return await _userManager.GetUserNameAsync(user);
        }

        public async Task<List<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }

        public async Task<bool> IsUniqueUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName) == null;
        }

        public async Task<ApiResponse> SigninUserAsync(LoginViewModel model)
        {
            ValidationResult results = await _validator.ValidateAsync(model);

            if (!results.IsValid)
            {
                return new ApiResponse(400, results.ToString());
            }

            var tenantid=model.TenantId;
            var testi = await _userManager.FindByNameAsync(model.Username);
            if (testi == null)
            {
                return new ApiResponse(400, "Ky User nuk egziston");
            }
            if(testi.TenantId != tenantid)
            {
                return new ApiResponse(400, "Ky User nuk egziston");
            }

            if (!results.IsValid)
            {
                return new ApiResponse(400, results.ToString());
            }

            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, true, false);
            if (result.Succeeded)
            {
                var (userId, fullName, userName, email, roles, tenantids) = await GetUserDetailsAsync(await GetUserIdAsync(model.Username));
                string token = _tokenGenerator.GenerateJWTToken((userId: userId, userName: model.Username, roles: roles,tenantId: tenantid.ToString()));
                var connectionId = await GetUserIdAsync(model.Username);
                var user= await GetUserDetailsAsync(connectionId.ToString());
                var tenant = _tenantAdminDbContext.Tenants.First(x => x.TenantId == user.tenantid);
                        return new ApiResponse(200, token);
            }
            return new ApiResponse(400, "Something went wrong");
        }

        public async Task<(string id, string roleName)> GetRoleByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return (role.Id, role.Name);
        }

        public async Task<ApiResponse> UpdateRole(string id, string roleName)
        {
            if (roleName != null)
            {
                var role = await _roleManager.FindByIdAsync(id);
                role.Name = roleName;
                var result = await _roleManager.UpdateAsync(role);
                return new ApiOkResponse(200);
            }
            return new ApiResponse(400);
        }

        public async Task<ApiResponse> UpdateUsersRole(string userName, IList<string> usersRole)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var existingRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, existingRoles);
            result = await _userManager.AddToRolesAsync(user, usersRole);

            return new ApiOkResponse(200);
        }

        public async Task<ApiResponse> ChangePasswordAsync(ChangePasswordViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return new ApiResponse(400, "User doesn't exist");
            }

            if (string.Compare(model.NewPassword, model.ConfirmPassword) != 0)
            {
                return new ApiResponse(400, "Passwords don't match");
            }
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                return new ApiResponse(400, "Something went wrong");
            }
            return new ApiResponse(200, "Password has been changed successfully");
        }

        public async Task<ApiResponse> ForgetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return new ApiResponse(400, "No user associated with email");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);
            var tenant=await _omContext.Tenants.Where(x => x.TenantId == user.TenantId).FirstOrDefaultAsync();

            string localHostUrl = "http://" +tenant.Name+ "localhost:7245/api/User";
            string url = $"{localHostUrl}/reset-password?email={email}&token={validToken}";


            var senderEmail = _configuration["ReturnPaths:SenderEmail"];

            await _userManager.FindByEmailAsync(email);
            await _emailRepository.SendEmailAsync(senderEmail, email, "Reset Password", "To reset the password click on the url: " +
              url);

            return new ApiResponse(200, "Reset password URL has been sent to the email successfully!");
        }

        public async Task<ApiResponse> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)

            {
                return new ApiResponse(400, "No user is associated with this email!");
            }

            if (model.NewPassword != model.ConfirmPassword)

            {
                return new ApiResponse(400, "Passwords do not match!");
            }

            var decodedToken = WebEncoders.Base64UrlDecode(model.token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);
            var result = await _userManager.ResetPasswordAsync(user, normalToken, model.NewPassword);
            
            if (!model.token.Equals(normalToken))
            {
                return new ApiResponse(400, "Token is not valid!");
            }
            
            if (result.Succeeded)
            {
                return new ApiResponse(200, "Password has been reset successfully!");
            }

            return new ApiResponse(400, "Something went wrong");
        }
    }
}