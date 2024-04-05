using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3Hattmakaren.Models
{
    public class Product_Material {
        //Kolla upp
        [Key]
        public int ProductId { get; set; }
        public int MaterialId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }


        [ForeignKey(nameof(MaterialId))] 
        public virtual Material Material { get; set; }



    }
}
