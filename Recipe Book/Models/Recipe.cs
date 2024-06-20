using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Recipe_Book.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }
        public string Name { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Time must be greater than 0.")]
        public int? PreparationTime { get; set; }
        public string? Ingredients { get; set; }
        public string? Instructions { get; set; }
        public int CategoryId { get; set; }

        [ValidateNever]
        public Category Category { get; set; } = null!;
        public int UserId { get; set; }

        [ValidateNever]
        public User User { get; set; } = null!;
    }
}
