using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3Hattmakaren.Models
{
    public class Product_Material {
        //Kolla upp
        public int productId { get; set; }
        public int materialId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product ProductId { get; set; }


        [ForeignKey(nameof(MaterialId))]
        public Material MaterialId { get; set; }

    }
}
