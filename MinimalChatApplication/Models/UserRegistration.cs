using System.ComponentModel.DataAnnotations;

namespace MinimalChatApplication.Models
{
    public class UserRegistration
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RegistrationDate { get; set; }
        public Boolean IsActive { get; set; }

        public ICollection<UserLogin> UserLogins { get; set; }

        public ICollection<Message> Messages { get; set; } 
    }
}
