using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApiApp.Data
{
    [Table("M_Product")]
    public class Product
    {
        [Key]
        [Required]
        public Guid ProducId { get; set; }
        [Required]
        [MaxLength(100)]
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        [Range(0, double.MaxValue)]
        public double Price {  get; set; }
        public byte Discount {  get; set; }
        public int? CategoryID {  get; set; }
        [ForeignKey(nameof(CategoryID))]
        public Category Category { get; set; }  
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public Product()
        {
            OrderDetails = new List<OrderDetail>();
        }

    }
}
