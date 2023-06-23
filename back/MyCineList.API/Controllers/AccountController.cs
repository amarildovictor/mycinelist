using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCineList.Domain.Entities.Auth;
using MyCineList.Domain.Interfaces.Services;

namespace MyCineList.API.Controllers
{
    [Route("v1/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAccessToken accessToken;

        public AccountController(UserManager<IdentityUser> userManager, IAccessToken accessToken)
        {
            this.accessToken = accessToken;
            this.userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            user.UserName = user.Email;

            var result = await userManager.CreateAsync(user, user.Password ?? string.Empty);

            if (!result.Succeeded)
            {
                Dictionary<string, string> errorList = new Dictionary<string, string>();

                foreach (var error in result.Errors)
                {
                    errorList.Add(error.Code, error.Description);
                }

                return NotFound(errorList);
            }

            user.Token = accessToken.GenerateAccessToken(user);
            user.Password = string.Empty;

            return Ok(user);
        }
    }
}