using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_MVC_VPD_121.Models;
using System.Diagnostics;

namespace Shop_MVC_VPD_121.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopDbContext context;

        public HomeController(ShopDbContext context)
        {
            this.context = context;
        }

        // action methods
        public IActionResult Index()
        {
            // ... code ...
            var products = context.Products.Include(x => x.Category).ToList();

            return View(products); // ~/Views/Home/Index.cshtml
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}