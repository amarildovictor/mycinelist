using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyCineList.Domain.Entities.Auth;
using MyCineList.Domain.Interfaces.Services;

namespace MyCineList.Domain.Services
{
    public class AccessToken : IAccessToken
    {
        private readonly JWTSettings JwtSettings;

        public AccessToken(IOptions<JWTSettings> jwtSettings)
        {
            this.JwtSettings = jwtSettings.Value;
        }

        public string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(JwtSettings.SecretKey ?? string.Empty);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email ?? string.Empty),
                    new Claim(ClaimTypes.NameIdentifier, user.Id ?? string.Empty)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }
    }
}