using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebApiApp.Data
{
    [Table("M_Category")]
    public class Category
    {
        [Key]
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(100)]
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
