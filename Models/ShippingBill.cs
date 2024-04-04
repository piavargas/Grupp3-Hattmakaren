using System.ComponentModel.DataAnnotations.Schema;

namespace Grupp3Hattmakaren.Models
{
    public class ShippingBill
    {
        public int ShippingBillId { get; set; }
        public string productCode { get; set; }

        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order order { get; set; }
    }
}
