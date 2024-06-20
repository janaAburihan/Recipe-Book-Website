using Microsoft.AspNetCore.Mvc;
using Recipe_Book.Models;
using System.Diagnostics;

namespace Recipe_Book.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
