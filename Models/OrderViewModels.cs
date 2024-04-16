namespace Grupp3Hattmakaren.Models
{
    public class OrderSummaryViewModel
    {
        public int OrderId { get; set; }
        public string name { get; set; }
        //public string Phone { get; set; }
        //kommenterade bort eftersom jag inte vet hur man hämtar telefonnummer från IdentityUser (Se OrderService.cs)
        public string email { get; set; }
        public List<ProductSummary> products { get; set; }
        public double TotalPrice { get; set; }
    }

    //Dubbelkolla hur man hämtar storlek. Just nu är det från produkt men kanske borde vara från förfrågan eller kund?
    public class ProductSummary
    {
        public string productName { get; set; }
        public string description { get; set; }
        public List<string> materials { get; set; }
        public double size { get; set; }
        public double price { get; set; }
    }
}
