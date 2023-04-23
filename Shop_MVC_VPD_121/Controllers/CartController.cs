using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Shop_MVC_VPD_121.Helpers;
using System.Text.Json;

namespace Shop_MVC_VPD_121.Controllers
{
    public class CartController : Controller
    {
        const string cartKey = "cartItems";
        private readonly ShopDbContext context;

        public CartController(ShopDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            // get all products in the cart
            var ids = HttpContext.Session.Get<List<int>>(cartKey);
            ids ??= new List<int>();

            List<Product?> products = ids.Select(id => context.Products.Find(id)).ToList();

            return View(products); 
        }

        public IActionResult Add(int id)
        {
            List<int>? ids = HttpContext.Session.Get<List<int>>(cartKey);

            // if id collection is null
            ids ??= new List<int>();

            ids.Add(id);

            // save product to cart
            HttpContext.Session.Set(cartKey, ids);

            return RedirectToAction("Index", "Home");
        }
    }
}
