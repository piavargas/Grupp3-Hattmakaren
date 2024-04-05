using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3Hattmakaren.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public double price { get; set; }

        public int CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]

        public virtual Customer Customer { get; set; }

        public int AddressId { get; set; }

        [ForeignKey(nameof(AddressId))]
        public virtual Address Address { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual ICollection<Product> products { get; set; }

  

    }
}
