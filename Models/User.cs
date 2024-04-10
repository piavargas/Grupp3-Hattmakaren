using Microsoft.AspNetCore.Identity;

namespace Grupp3Hattmakaren.Models
{
    public class User : IdentityUser
    {
        // Gemensamma egenskaper för alla användare
        public string firstName { get; set; }
        public string lastName { get; set; }


        // Egenskaper för administratörer
        public string employerCode { get; set; }

        // Egenskaper för kunder
        public string headSize { get; set; }
        public virtual ICollection<Order> orders { get; set; }
        public virtual ICollection<Address> addresses { get; set; }
    }

}
