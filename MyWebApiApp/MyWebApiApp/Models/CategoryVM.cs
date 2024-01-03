using System.ComponentModel.DataAnnotations;

namespace MyWebApiApp.Models
{
    public class CategoryVM
    {
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
    }
}
