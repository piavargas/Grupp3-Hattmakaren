using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3Hattmakaren.Models
{
    public class Enquiry
    {
        [Key]
        public int EnquiryId { get; set; }
        public bool expressDelivery { get; set; }

        //Samtycke att bygga vidare på en existerande hatt 
        public bool consentHat { get; set; }
        public string description { get; set; }
        public string referenceImage { get; set; }

        public int CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; }
    }
}
