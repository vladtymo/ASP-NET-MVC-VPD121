using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using Shop_MVC_VPD_121.Helpers;
using Microsoft.AspNetCore.Authorization;
using Shop_MVC_VPD_121.Models;
using Shop_MVC_VPD_121.Services;

namespace Shop_MVC_VPD_121.Controllers
{
    [Authorize(Roles = "Admin")] // give an access to admins only
    public class ProductsController : Controller
    {
        // readonly - can initialize or set in constructor only
        private readonly ShopDbContext context;
        private readonly IAzureStorageService storageService;

        public ProductsController(ShopDbContext context, IAzureStorageService storageService)
        {
            this.context = context;
            this.storageService = storageService;
        }

        private void LoadCategories()
        {
            // ViewData dictionary colleciton
            //ViewData["CategoryList"] = ...

            // ViewBag - dynamic property collection
            ViewBag.CategoryList = new SelectList(context.Categories.ToList(), "Id", "Name");
            //ViewBag.Username = "vladtymo";
        }

        // GET: ~/products/index
        [HttpGet] // by default
        [AllowAnonymous] // give an access to any users
        public IActionResult Index()
        {
            // get products from database

            // Include() - LEFT JOIN operator
            List<Product> products = context.Products.Include(x => x.Category).ToList();

            return View(products);
        }

        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var item = context.Products.Find(id);

            if (item == null)
                return NotFound();

            return View(item);
        }

        public IActionResult Sort(string property)
        {
            List<Product> products = context.Products
                .Include(x => x.Category)
                .OrderBy(property)
                .ToList();

            return View("Index", products);
        }

        // open the page for create new product
        [HttpGet]
        public IActionResult Create()
        {
            LoadCategories();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductModel product)
        {
            // model validation
            if (!ModelState.IsValid)
            {
                LoadCategories();
                return View("Create");
            }

            // create product entity object
            Product entity = new()
            {
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Description = product.Description,
                InStock = product.InStock,
                Discount = product.Discount
            };

            // upload image to storage
            if (product.Image != null)
                entity.ImageUrl = await storageService.UploadAsync(product.Image);

            // add to the database
            context.Products.Add(entity);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var product = context.Products.Find(id);
            if (product == null) return NotFound();

            LoadCategories();

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories();
                return View("Edit");
            }

            // update product in the database
            context.Products.Update(product);
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
