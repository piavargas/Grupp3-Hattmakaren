using Microsoft.AspNetCore.Identity;

namespace Grupp3Hattmakaren.Models
{
    public class User : IdentityUser
    {
        public string firstName { get; set; }
        public string lastName { get; set; }

        public virtual ICollection<Order> orders { get; set; }
        public virtual ICollection<Address> addresses { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

    }

}
