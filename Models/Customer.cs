using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3Hattmakaren.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string headSize { get; set; }

        public virtual ICollection<Order> orders { get; set; }
        public virtual ICollection<Address> addresses { get; set; }

    }
}
