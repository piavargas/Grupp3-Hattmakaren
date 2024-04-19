using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3Hattmakaren.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public string CustomerId {  get; set; }
        [ForeignKey(nameof(CustomerId))]

        public virtual Customer Customer { get; set; }
        public string? streetName { get; set; }
        public int? zipCode { get; set; }
        public string? countryName { get; set; }
    }
}
