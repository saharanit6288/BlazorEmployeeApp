using BlazorEmployeeManagementApp2.Shared.AuthModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlazorEmployeeManagementApp2.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountsController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterModel model)
        {
            var newUser = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                EmailConfirmed = true,
                PhoneNumberConfirmed= true,
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return Ok(new RegisterResult { Successful = false, Errors = errors });

            }

            return Ok(new RegisterResult { Successful = true });
        }

        [HttpGet]
        [Route("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserName = currentUser.FindFirstValue(ClaimTypes.Name);
            var user = await _userManager.FindByNameAsync(currentUserName);

            UserDetails usrDtl = new UserDetails();
            if (user != null)
            {
                usrDtl.Email = user.Email;
                usrDtl.EmailConfirmed = user.EmailConfirmed;
                usrDtl.PhoneNumber = user.PhoneNumber;
                usrDtl.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                usrDtl.UserName = user.UserName;
            }
            return Ok(usrDtl);
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var currentUserName = User.FindFirstValue(ClaimTypes.Name);
            var user = await _userManager.FindByNameAsync(currentUserName);

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return Ok(new ChangePasswordResult { Successful = false, Errors = errors });

            }

            return Ok(new ChangePasswordResult { Successful = true });
        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return Ok(new ResetPasswordResult { Successful = false, Errors = errors });

            }

            return Ok(new ResetPasswordResult { Successful = true });
        }
    }
}
