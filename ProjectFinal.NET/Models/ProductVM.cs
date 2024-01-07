namespace ProjectFinal.NET.Models
{
    public class ProductVM
    {
        public int IdProduct { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public int PriceNew  { get; set; }
        public int PriceOld { get; set;}
        public int Quantity {  get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Cate { get; set; }
        
    }
}
