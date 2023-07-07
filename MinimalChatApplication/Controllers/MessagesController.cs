using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalChatApplication.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;


namespace MinimalChatApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly AppDbContext _Context;
        public MessagesController(AppDbContext dbContext)
        {
            _Context = dbContext;

        }

        [HttpPost("api/messages")]
        public async Task<IActionResult> SendMessage(int receiverId, string content)
        {
            // Get the sender's user ID from the claims
            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Validate the input parameters
            if (receiverId <= 0)
            {
                return BadRequest(new { error = "Invalid receiver ID" });
            }

            if (string.IsNullOrEmpty(content))
            {
                return BadRequest(new { error = "Message content is required" });
            }

            // Check if the receiver user exists
            var receiver = await _Context.UserRegistrations.FindAsync(receiverId);
            if (receiver == null)
            {
                return BadRequest(new { error = "Receiver user not found" });
            }

            // Create a new message
            var message = new Message
            {
                UserId = Convert.ToInt32(senderId),
                ReceiverId = receiverId,
                Content = content,
                Timestamp = DateTime.UtcNow
            };

            // Add the message to the database
            _Context.Messages.Add(message);
            await _Context.SaveChangesAsync();

            // Return the message details in the response
            return Ok(new
            {
                messageId = message.MessageId,
                senderId = message.UserId,
                receiverId = message.ReceiverId,
                content = message.Content,
                timestamp = message.Timestamp
            });
        }

    }
}
