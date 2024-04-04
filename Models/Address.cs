using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3Hattmakaren.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        [ForeignKey(nameof(CustomerId))]

        public Customer CustomerId { get; set; }

        public string addressType { get; set; }
        public string streetName { get; set; }
        public int zipCode { get; set; }
        public string cityName { get; set; }
        public string countryName { get; set; }
    }
}
