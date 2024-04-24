using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace Grupp3Hattmakaren.Models
{
    public class Customer : User
    {

        public string headSize { get; set; }

        public virtual ShoppingCart cart { get; set; }

        public virtual ICollection<Order> orders { get; set; }
        public virtual ICollection<Address> addresses { get; set; }

    }
}
