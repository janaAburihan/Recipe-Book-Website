using System.ComponentModel.DataAnnotations;

namespace Recipe_Book.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? Password { get; set; }
        public string? Email { get; set; }
    }
}
