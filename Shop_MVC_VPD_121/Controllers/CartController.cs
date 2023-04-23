using Data;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Shop_MVC_VPD_121.Helpers;
using Shop_MVC_VPD_121.Services;
using System.Text.Json;

namespace Shop_MVC_VPD_121.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopDbContext context;
        private readonly ICartService cartService;

        public CartController(ShopDbContext context, ICartService cartService)
        {
            this.context = context;
            this.cartService = cartService;
        }
        public IActionResult Index()
        {
            return View(cartService.GetAll()); 
        }

        public IActionResult Add(int id)
        {
            cartService.Add(id);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Remove(int id)
        {
            cartService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
