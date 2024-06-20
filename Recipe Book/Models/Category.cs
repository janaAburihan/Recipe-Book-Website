using System.ComponentModel.DataAnnotations;

namespace Recipe_Book.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
