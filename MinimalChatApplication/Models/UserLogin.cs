using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalChatApplication.Models
{
    public class UserLogin
    {
        [Key]
        public int LoginID { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [ForeignKey("UserId")]
        public UserRegistration User {  get; set; }
        
    }
}
