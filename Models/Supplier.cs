using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3Hattmakaren.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public int MaterialId { get; set; }
        //Ändra länk om det behövs
        public string link { get; set; }

        [ForeignKey(nameof(MaterialId))]

        public virtual Material Material { get; set; }
    }
}
