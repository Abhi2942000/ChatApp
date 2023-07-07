using System.ComponentModel.DataAnnotations;

namespace MinimalChatApplication.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public int UserId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
