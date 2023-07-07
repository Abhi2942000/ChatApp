using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MinimalChatApplication.HelperFunction;
using System.Security.Claims;

using System.IdentityModel.Tokens.Jwt;
using MinimalChatApplication.Models;

namespace MinimalChatApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserloginController : ControllerBase
    {
        private readonly AppDbContext _Context;
        private readonly IConfiguration _Configuration;
       
        

        public UserloginController(AppDbContext dbContext,IConfiguration configuration)
        {
            _Context = dbContext;
            _Configuration = configuration;
            
        }

        [HttpPost]
        public async Task<IActionResult> userlogin(string Email, string Password)
        {
            if (Email == null && Password == null)
            {
                return BadRequest();
            }
            var user = await _Context.UserRegistrations.FirstOrDefaultAsync(ul => ul.Email == Email);
            if (user == null)
            {
                return NotFound("User Not Found");
            }
            if (!Helper.VerifyPassword(Password, user.Password))
            {
                return BadRequest("Password is Incorrect");
            }
            string token = CreateToken(user);
            return Ok(token);
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
