using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3Hattmakaren.Models
{
    public class Enquiry
    {
        [Key]
        public int EnquiryId { get; set; }

        public string? headSize { get; set; }
        public bool consentHat { get; set; } //Samtycke att bygga vidare på en existerande hatt 
        public string? description { get; set; }

        public  string? fabricMaterial { get; set; } // Enquiry class
        public  string?  specialEffectMaterials { get; set; } // Enquiry class

        //public string referenceImage { get; set; }
        public string? color { get; set; }
        public string? font { get; set; }
        public string? textOnHat { get; set; }
        public bool isInProgress { get; set; }
        public bool isSpecial { get; set; }

        public bool getInStore { get; set; }
        public string CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; }
    }
}
