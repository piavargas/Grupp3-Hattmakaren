using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Grupp3Hattmakaren.Models
{
    public class SendMessageViewModel
    {
        public int MessageId { get; set; }
        public string messageText { get; set; }
        public bool isRead { get; set; }
        public string sender { get; set; }

        public List<Customer>? Customers { get; set; }
        public int CustomerId { get; set; }
        public string UserId { get; set; }


    }
}
