using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Recipe_Book.Models
{
    public class RecipesContext:DbContext
    {
        public RecipesContext(DbContextOptions<RecipesContext> options):base(options)

        { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Recipe> Recipes { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Main Meal"
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Deserts"
                },
                new Category
                {
                    CategoryId = 3,
                    Name = "Appetizers"
                },
                new Category
                {
                    CategoryId = 4,
                    Name = "Drinks"
                },
                new Category
                {
                    CategoryId = 5,
                    Name = "Side Dishes"
                },
                new Category
                {
                    CategoryId = 6,
                    Name = "Snacks"
                }
                );
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Name = "Jana",
                    Password = 1234,
                    Email = "Jana@gmail.com"
                },
                new User
                {
                    UserId = 2,
                    Name = "Raghad",
                    Password = 1234,
                    Email = "Raghad@gmail.com"
                },
                new User
                {
                    UserId = 3,
                    Name = "Joud",
                    Password = 1234,
                    Email = "Joud@gmail.com"
                });
            modelBuilder.Entity<Recipe>().HasData(
           new Recipe
           {
               RecipeId = 1,
               Name = "Chocolate Cake",
               PreparationTime = 60,
               Ingredients = "Eggs, Sugar, Flour, Cocoa, Baking Powder, Milk",
               Instructions = "Mix all ingredients together till you get a smooth mixture. Bake it for 40-50 minutes, cool, and serve.",
               CategoryId = 2,
               UserId = 1
           },
           new Recipe
           {
               RecipeId = 2,
               Name = "Pasta",
               PreparationTime = 40,
               Ingredients = "Pasta, Tomato, Onion, Olive Oil, Salt, Water, Mushroom, Olives",
               Instructions = "",
               CategoryId = 1,
               UserId = 2
           },
           new Recipe
           {
               RecipeId = 3,
               Name = "Fish and Chips",
               PreparationTime = 50,
               Ingredients = "",
               Instructions = "",
               CategoryId = 1,
               UserId = 1
           }

       );
        }
    }
}

