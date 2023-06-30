using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyCineList.Domain.Entities.Auth;
using MyCineList.Domain.Interfaces.Services;

namespace MyCineList.API.Controllers
{
    /// <summary>
    /// Auth Controller class.
    /// </summary>
    [Route("v1/[controller]")]
    public class AuthController : Controller
    {
        private readonly JWTSettings JwtSettings;
        private readonly IAccessToken AccessToken;
        private UserManager<User> UserManager { get; }

        /// <summary>
        /// Auth Controller constructor.
        /// </summary>
        /// <param name="jwtSettings">JWT instance.</param>
        /// <param name="accessToken">Access token instance.</param>
        /// <param name="userManager">User manager instance.</param>
        public AuthController(IOptions<JWTSettings> jwtSettings, IAccessToken accessToken, UserManager<User> userManager)
        {
            this.UserManager = userManager;
            this.AccessToken = accessToken;
            this.JwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// User authentication method.
        /// </summary>
        /// <param name="model">User model.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Authenticate([FromBody] User model)
        {
            if (model.Email != null && model.Password != null)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);

                if (user == null || !await UserManager.CheckPasswordAsync(user, model.Password))
                {
                    return NotFound(new { message = "E-mail ou senha inválidos." });
                }

                string token = AccessToken.GenerateAccessToken(user);
                user.Password = null;
                user.Token = token;

                return user;
            }
            else
            {
                return NotFound(new { message = "O e-mail e a senha devem ser informados." });
            }
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