using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalChatApplication.Models;
using MinimalChatApplication;
using MinimalChatApplication.HelperFunction;

namespace Minimal_Chat_Application.Controllers
{
    [Route("api/[controller]")]   //it is decorated with [Route("api/[controller]")]
    [ApiController]
    public class UserRegController : ControllerBase
    {
        private readonly AppDbContext _Context;
        public UserRegController(AppDbContext dbContext)
        {
            _Context = dbContext;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserRegistration>>> GetUser()
        {
            if (_Context.UserRegistrations == null)
            {
                return NotFound();
            }
            return await _Context.UserRegistrations.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult> AddUser(string Name, string Email, string Password)
        {
            if (await _Context.UserRegistrations.AnyAsync(ur => ur.Email == Email))
            {
                return Conflict(new { error = "Email is Already Registred" });
            }
            var userRegistration = new UserRegistration
            {
                Name = Name,
                Email = Email,
                Password = Helper.HashPassword(Password),
                RegistrationDate = Convert.ToString(DateTime.Now.Date),
                IsActive = true
            };
            _Context.UserRegistrations.Add(userRegistration);
            await _Context.SaveChangesAsync();
            
            return Ok("User Registration Successfully");
        }
    }
}