using System.ComponentModel.DataAnnotations;

namespace Grupp3Hattmakaren.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string productName { get; set; }
        public string description { get; set; }
        public double size { get; set; }
        public virtual ICollection<Order> orders { get; set; }
        public virtual ICollection<Material> materials { get; set; }
    }
}
