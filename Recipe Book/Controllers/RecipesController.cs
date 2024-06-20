using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipe_Book.Models;

namespace Recipe_Book.Controllers
{
    public class RecipesController : Controller
    {
        private RecipesContext context { get; set; }
        public RecipesController(RecipesContext ctx) => context = ctx;
        public IActionResult RecipesList()
        {
            int? userID = HttpContext.Session.GetInt32("userid");
            if (userID != null) {
                var recipes = context.Recipes
               .Include(m => m.Category)
               .Include(m => m.User)
               .OrderBy(m => m.Name)
               .ToList();
                return View(recipes);
            }
            return RedirectToAction("Login","User");
            
        }

        public IActionResult View(int id)
        {
            ViewBag.Categories = context.Categories.OrderBy(m => m.Name).ToList();
            int? userID = HttpContext.Session.GetInt32("userid");
            if (userID != null)
            {
                var recipe = context.Recipes.Find(id);
                return View(recipe);
            }
            return RedirectToAction("Login", "User");

        }
        [HttpPost]
        public IActionResult Search(string? Name, string? Category, string? Owner)
        {
            ViewBag.Categories = context.Categories.OrderBy(g => g.Name).ToList();
            List<Recipe> rp = context.Recipes.Where(m => (m.Name.Contains(Name != null ? Name : "") && m.Category.Name.Contains(Category != null ? Category : "") && m.User.Name.Contains(Owner != null ? Owner : ""))).ToList();
            if (rp != null)
            {
                int? userID = HttpContext.Session.GetInt32("userid");
                if (userID != null)
                {
                    var recipes = context.Recipes
                   .Include(m => m.Category)
                   .Include(m => m.User)
                   .OrderBy(m => m.Name)
                   .ToList();
                    return View("RecipesList", rp);
                }
            }
            return RedirectToAction("Index", "User");
        }
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Categories = context.Categories.OrderBy(g => g.Name).ToList();
            return View("Edit", new Recipe());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Categories = context.Categories.OrderBy(g => g.Name).ToList();
            var movie = context.Recipes.Find(id);
            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(Recipe recipe)
        {
            ViewBag.Categories = context.Categories.OrderBy(g => g.Name).ToList();
            if (ModelState.IsValid)
            {
                if (recipe.RecipeId == 0)
                    context.Recipes.Add(recipe);
                else
                    context.Recipes.Update(recipe);
                context.SaveChanges();
                return RedirectToAction("UserRecipes", "User");
            }
            else
            {
                int recipeId = recipe.RecipeId;
                ViewBag.Action = recipeId == 0 ? "Add" : "Edit";
                ViewBag.Categories = context.Categories.OrderBy(g => g.Name).ToList();
                ViewBag.userID = HttpContext.Session.GetInt32("userid");
                return View(recipe);
            }
        }

       
        public IActionResult Delete(int id)
        {
            Recipe r = context.Recipes.Find(id);
            if (r != null)
            {
                context.Recipes.Remove(r);
                context.SaveChanges();
            }
            int? ID = HttpContext.Session.GetInt32("userid");
            User usr = context.Users.Find(ID);
            List<Recipe> recipes = context.Recipes.Where(m => m.UserId == usr.UserId).ToList();

            return RedirectToAction("UserRecipes","User", recipes);

        }
    }
    }

