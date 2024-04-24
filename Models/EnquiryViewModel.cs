using System.ComponentModel.DataAnnotations;

namespace Grupp3Hattmakaren.Models
{
    public class EnquiryViewModel
    {

        [RegularExpression(@"^\d+$", ErrorMessage = "Please specify a valid head size (only numbers allowed)")]
        [Required(ErrorMessage ="The head size is required")]
        public string? headSize { get; set; }// Customer class


        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Please specify a valid color (only letters allowed)")]
        [Required(ErrorMessage ="The color of the hat is required")]
        public string? color { get; set; } // Enquiry class
       
        [Required(ErrorMessage = "Fabric material for the hat is required")]
        public string? fabricMaterial { get; set; } // Enquiry class
        public string? specialEffectMaterials { get; set; } // Enquiry class

        public string? textOnHat { get; set; } // Enquiry class

        public string? font { get; set; }  // Enquiry class
        public string? description { get; set; } // Enquiry class

        //public string referenceImageFile { get; set; } // Enquiry class
        public bool? consentHat { get; set; } // Enquiry class
        public string firstName { get; set; }  // Customer class
        public string lastName { get; set; } // Customer class

        public string? email { get; set; } // Customer class


        public string? streetName { get; set; } // Adress class
        public int? zipCode { get; set; } // Adress class
        public string? countryName { get; set; } // Adress class
        public bool isSpecial { get; set; }
        public bool isInProgress { get; set; } // Enquiry class
        public bool getInStore { get; set; } 





    }
}
