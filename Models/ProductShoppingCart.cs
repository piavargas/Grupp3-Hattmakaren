using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3Hattmakaren.Models
{
    public class ProductShoppingCart
    {
        public int productId { get; set; }

        public int shoppingCartId { get; set; }
        [ForeignKey(nameof(productId))]
        public virtual Product product { get; set; }
        [ForeignKey(nameof(shoppingCartId))]
        public virtual ShoppingCart shoppingCart { get; set; }

        public int quantity { get; set; }

    }
}
