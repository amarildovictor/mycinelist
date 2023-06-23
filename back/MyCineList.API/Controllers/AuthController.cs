using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyCineList.Domain.Entities.Auth;
using MyCineList.Domain.Interfaces.Services;

namespace MyCineList.API.Controllers
{
    [Route("v1/[controller]")]
    public class AuthController : Controller
    {
        private readonly JWTSettings JwtSettings;
        private readonly IAccessToken AccessToken;
        public UserManager<User> UserManager { get; }

        public AuthController(IOptions<JWTSettings> jwtSettings, IAccessToken accessToken, UserManager<User> userManager)
        {
            this.UserManager = userManager;
            this.AccessToken = accessToken;
            this.JwtSettings = jwtSettings.Value;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Authenticate([FromBody] User model)
        {
            if (model.Email != null && model.Password != null)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);

                if (user == null || await UserManager.CheckPasswordAsync(user, model.Password))
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

        [HttpGet]
        [Route("anonimo")]
        [AllowAnonymous]
        public IActionResult Anonimo()
        {
            return Ok("Tem permissão!");
        }

        [HttpGet]
        [Route("autenticado")]
        [Authorize]
        public IActionResult Autenticado()
        {
            return Ok($"Tem permissão! Usuário: {User.Identity?.Name}");
        }
    }
}