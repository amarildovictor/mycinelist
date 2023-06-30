using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyCineList.Domain.Entities.Auth;
using MyCineList.Domain.Interfaces.Services;

namespace MyCineList.API.Controllers
{
    /// <summary>
    /// Account Controller class.
    /// </summary>
    [ApiController]
    [Route("v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IAccessToken accessToken;

        /// <summary>
        /// Account Controller constructor.
        /// </summary>
        /// <param name="userManager">User manager instance.</param>
        /// <param name="accessToken">Access token instance.</param>
        public AccountController(UserManager<User> userManager, IAccessToken accessToken)
        {
            this.accessToken = accessToken;
            this.userManager = userManager;
        }

        /// <summary>
        /// Method to register (create) an user.
        /// </summary>
        /// <param name="user">User model.</param>
        /// <returns>Action result.</returns>
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

        /// <summary>
        /// Method to test anonymous access.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        [Route("anonimo")]
        [AllowAnonymous]
        public IActionResult Anonimo()
        {
            return Ok("Tem permissão!");
        }

        /// <summary>
        /// Method to test the jwt token validation access.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        [Route("autenticado")]
        [Authorize]
        public IActionResult Autenticado()
        {
            return Ok($"Tem permissão! Usuário: {User.Identity?.Name}");
        }
    }
}