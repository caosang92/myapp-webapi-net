namespace MyWebApiApp.Models
{
    public class ProductVM
    {
        public string? ProductName { get; set; }
        public double Price { get; set; }
        public byte Discount {  get; set; }
        public int? CategoryID {  get; set; }

    }
}
