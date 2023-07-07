using Microsoft.Extensions.Configuration;
using MinimalChatApplication.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace MinimalChatApplication.HelperFunction
{
    public class JwtToken
    {
        private readonly IConfiguration _Configuration;

        public JwtToken(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        private string CreateToken(UserRegistration user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
                //new Claim("UserId", user.UserID.ToString())
                //new Claim(ClaimTypes.Role,"User")
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_Configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
