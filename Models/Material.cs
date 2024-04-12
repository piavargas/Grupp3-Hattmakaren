using System.ComponentModel.DataAnnotations;

namespace Grupp3Hattmakaren.Models
{
    public class Material
    {
        [Key]
        public int materialId {  get; set; }
        [Required]

        public string name { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]

        public string supplier { get; set; }
        [Required]

        public double price { get; set; }

    }
}
