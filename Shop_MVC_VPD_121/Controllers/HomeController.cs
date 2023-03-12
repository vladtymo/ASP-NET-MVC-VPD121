using Microsoft.AspNetCore.Mvc;
using Shop_MVC_VPD_121.Models;
using System.Diagnostics;

namespace Shop_MVC_VPD_121.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // ... code ...
            return View(); // ~/Views/Home/Index.cshtml
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