using System.ComponentModel.DataAnnotations;

namespace MinimalChatApplication.Models
{
    public class SendMessageRequest
    {
        //public string MessageId { get; set; }   
        [Required]
        [Key]
        public int ReceiverId { get; set; }

        public string Content { get; set; }
    }
}
