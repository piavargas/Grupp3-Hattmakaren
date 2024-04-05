using System.ComponentModel.DataAnnotations;

namespace Grupp3Hattmakaren.Models
{
    public class Material
    {
        [Key]
        public int materialId {  get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public double price { get; set; }
    }
}
