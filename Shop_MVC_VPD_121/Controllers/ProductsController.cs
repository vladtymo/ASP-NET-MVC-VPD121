using Microsoft.AspNetCore.Mvc;
using Shop_MVC_VPD_121.Models;

namespace Shop_MVC_VPD_121.Controllers
{
    public class ProductsController : Controller
    {
        static List<Product> products = new List<Product>()
        {
            new Product() { Id = 1, Name = "MacBook Pro 2019", Price = 1300, Category = "Electronics" },
            new Product() { Id = 2, Name = "Samsung S23 Ultra", Price = 950, Category = "Electronics" },
            new Product() { Id = 3, Name = "Adidas T-Shirt", Price = 320, Category = "Clothes" },
            new Product() { Id = 4, Name = "Google Glass 2", Price = 670, Category = "Accesories" }
        };

        public ProductsController()
        {
        }

        // GET: ~/products/index
        [HttpGet]
        public IActionResult Index()
        {
            // get products from database

            return View(products);
        }

        public IActionResult Delete(int id)
        {
            var item = products.FirstOrDefault(x => x.Id == id);

            if (item == null)
                return NotFound();

            products.Remove(item);

            return RedirectToAction("Index");
        }
    }
}
