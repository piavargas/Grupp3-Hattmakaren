using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3Hattmakaren.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public string customerId { get; set; }

        [ForeignKey(nameof(customerId))]
        public virtual Customer customer { get; set; }

        public int productId { get; set; }
        public virtual ICollection<Product>? products { get; set; } = new List<Product>();
    }

}
