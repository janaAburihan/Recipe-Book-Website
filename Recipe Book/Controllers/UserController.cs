using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Recipe_Book.Models;

namespace Recipe_Book.Controllers
{
    public class UserController : Controller
    {
        public RecipesContext ctx { get; set; }
        public UserController(RecipesContext ctx) { this.ctx = ctx; }

        public IActionResult Index()
        {
            int? ID = HttpContext.Session.GetInt32("userid");
            User usr = ctx.Users.Find(ID);
            ViewBag.Categories = ctx.Categories.OrderBy(g => g.Name).ToList();
            return View(usr);
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(User mv)
        {
            ctx.Users.Add(mv);
            ctx.SaveChanges();
            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User s)
        {
            User usr = ctx.Users.Where(m => (m.Name == s.Name && m.Password == s.Password)).FirstOrDefault();
            if (usr != null)
            {
                ViewBag.Categories = ctx.Categories.OrderBy(g => g.Name).ToList();
                HttpContext.Session.SetInt32("userid", usr.UserId);
                return View("Index", usr);
            }
            else
                return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Login");
        }
        public IActionResult ShowDetails()
        {
            if (HttpContext.Session.GetInt32("userid") == null)
                return RedirectToAction( "Login");

            int? ID = HttpContext.Session.GetInt32("userid");

            User mv = ctx.Users.Find(ID);
            return View("ShowDetails", mv);
        }
        [HttpGet]
        public IActionResult Change()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Change(int Password)
        {
            if (HttpContext.Session.GetInt32("userid") == null)
                return View("Login");

            int? ID = HttpContext.Session.GetInt32("userid");
            User U = ctx.Users.Find(ID);
            U.Password = Password;
            ctx.Users.Update(U);
            ctx.SaveChanges();
            return View("ShowDetails", U);
        }

        public IActionResult UserRecipes()
        {
            int? userID = HttpContext.Session.GetInt32("userid");
            if (userID != null)
            {
                User usr = ctx.Users.Find(userID);
                var recipes = ctx.Recipes
               .Include(m => m.Category)
               .Include(m => m.User)
               .Where(m => m.UserId == usr.UserId)
               .OrderBy(m => m.Name)
               .ToList();
                return View(recipes);
            }
            return RedirectToAction("Login", "User");

        }

        public IActionResult ViewRecipe(int id)
        {
            ViewBag.Categories = ctx.Categories.OrderBy(m => m.Name).ToList();
            int? userID = HttpContext.Session.GetInt32("userid");
            if (userID != null)
            {
                var recipe = ctx.Recipes.Find(id);
                return View(recipe);
            }
            return RedirectToAction("Login", "User");

        }

    }

}


