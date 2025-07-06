using System.ComponentModel.DataAnnotations;

namespace MyWebApiApp.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
    }
}
