using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3Hattmakaren.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public string text { get; set; }
        public bool isRead { get; set; }
        public bool isAccepted { get; set; }
        public string sender { get; set; }

        public string Id { get; set; }
        [ForeignKey(nameof(Id))]
        public virtual User User { get; set; }
    }
}
