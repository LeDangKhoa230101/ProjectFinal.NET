namespace ProjectFinal.NET.Models
{
    public class CartItem
    {
        public int IdProduct { get; set; }
        public string? ProductName { get; set; } 
        public string? Image { get; set; }
        public int Price { get; set; } 
        public int Quantity { get; set; }
        public double TotalPrice => Quantity * Price;
    }
}
