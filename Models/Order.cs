using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3Hattmakaren.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public double price { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer CustomerId { get; set; }

        [ForeignKey(nameof(AddressId))]
        public Address AddressId { get; set; }
        public Product ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public ICollection<Product> products { get; set; }

  

    }
}
