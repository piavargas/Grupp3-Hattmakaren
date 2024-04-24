using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3Hattmakaren.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public double price { get; set; }
        public bool isPayed { get; set; }

        public string CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]

        public virtual Customer Customer { get; set; }

        public int AddressId { get; set; }

        [ForeignKey(nameof(AddressId))]
        public virtual Address Address { get; set; }

        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public int EnquiryId { get; set; }

        [ForeignKey(nameof(EnquiryId))]
        public virtual Enquiry Enquiry { get; set; }
        public virtual ICollection<Product> products { get; set; }

        public virtual ICollection<ShippingBill> ShippingBills { get; set; }

    }
}
