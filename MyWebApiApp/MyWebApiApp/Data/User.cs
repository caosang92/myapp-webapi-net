using System.ComponentModel.DataAnnotations;

namespace MyWebApiApp.Data
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        public string Name {  get; set; }
        public string Email { get; set; }





    }
}
