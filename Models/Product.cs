using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3Hattmakaren.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity) ]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Please type in the name of the product.")]
        public string productName { get; set; }
        [Required(ErrorMessage = "Please type in the product description")]
        public string description { get; set; }
        [Required(ErrorMessage = "Vänligen skriv Storlek.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Size has to be of a numeric value")]

        public double size { get; set; }
        [Required(ErrorMessage = "Vänligen skriv Pris.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Price has to be of a numeric value")]

        public double price { get; set; }

        public virtual ICollection<Order> ?orders { get; set; }
        public virtual ICollection<Material> ?materials { get; set; }
    }
}
