using System.ComponentModel.DataAnnotations;

namespace Grupp3Hattmakaren.Models
{
    public class Material
    {
        [Key]
        public int materialId {  get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-zÅÄÖåäö]+$", ErrorMessage = "The name of the material must contain only letters")]
        public string name { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Quanity must contain only numbers")]

        public int quantity { get; set; }
        [Required]

        public string supplier { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Price must contain only numbers")]

        public double price { get; set; }

    }
}
