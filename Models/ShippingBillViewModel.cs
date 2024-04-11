namespace Grupp3Hattmakaren.Models
{
    public class ShippingBillViewModel
    {
        public string productCode { get; set; } //Behövs hårdkodas? 6504 00 00
        public string customerFullName { get; set; }
        public string addressDetails { get; set; }
    }
}
