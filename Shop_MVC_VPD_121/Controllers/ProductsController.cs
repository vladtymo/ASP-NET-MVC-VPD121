using Microsoft.AspNetCore.Mvc;
using Shop_MVC_VPD_121.Data;
using Shop_MVC_VPD_121.Entities;

namespace Shop_MVC_VPD_121.Controllers
{
    public class ProductsController : Controller
    {
        // readonly - can initialize or set in constructor only
        private readonly ShopDbContext context;
        public ProductsController(ShopDbContext context)
        {
            this.context = context;
        }

        // GET: ~/products/index
        [HttpGet] // by default
        public IActionResult Index()
        {
            // get products from database
            List<Product> products = context.Products.ToList();

            return View(products);
        }

        public IActionResult Details(int id)
        {
            var item = context.Products.Find(id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        // open the page for create new product
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            // TODO: add validation
            if (!ModelState.IsValid)
            {
                return View("Create");
            }

            // add to the database
            context.Products.Add(product);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var item = context.Products.Find(id);

            if (item == null)
                return NotFound();

            context.Products.Remove(item);
            context.SaveChanges(); // sumbit all changes to database

            return RedirectToAction("Index");
        }
    }
}
