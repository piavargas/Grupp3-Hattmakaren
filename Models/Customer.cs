using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace Grupp3Hattmakaren.Models
{
    //hejdå 
    public class Customer : IdentityUser
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string headSize { get; set; }

        public virtual ICollection<Order> orders { get; set; }
        public virtual ICollection<Address> addresses { get; set; }

    }
}
