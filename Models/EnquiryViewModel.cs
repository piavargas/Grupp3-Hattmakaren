using System.ComponentModel.DataAnnotations;

namespace Grupp3Hattmakaren.Models
{
    public class EnquiryViewModel
    {
        public string headSize { get; set; } // Customer class
        public string color { get; set; }
        public string materialName { get; set; } // Material class 
        public string textOnHat { get; set; } // Enquiry class
        public string font { get; set; }  // Enquiry class
        public string description { get; set; } // Enquiry class

        //public string referenceImageFile { get; set; } // Enquiry class
        public bool consentHat { get; set; } // Enquiry class
        public string firstName { get; set; }  // Customer class
        public string lastName { get; set; } // Customer class
        public string email { get; set; } // Customer class
        public string streetName { get; set; } // Adress class
        public int zipCode { get; set; } // Adress class
        public string countryName { get; set; } // Adress class
        public bool isSpecial { get; set; }
        public bool isInProgress { get; set; } // Enquiry class



    }
}
