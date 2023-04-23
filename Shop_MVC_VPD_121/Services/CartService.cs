using Data;
using Data.Entities;
using Microsoft.AspNetCore.Http;
using Shop_MVC_VPD_121.Helpers;
using System.Globalization;

namespace Shop_MVC_VPD_121.Services
{
    public interface ICartService
    {
        void Add(int id);
        void Remove(int id);
        List<Product?> GetAll();
        void Clear();
        int GetCount();
    }

    public class CartService : ICartService
    {
        const string cartKey = "cartItems";
        private readonly HttpContext httpContext;
        private readonly ShopDbContext context;

        public CartService(IHttpContextAccessor httpContextAccessor, ShopDbContext context)
        {
            this.httpContext = httpContextAccessor.HttpContext;
            this.context = context;
        }

        public void Add(int id)
        {
            List<int>? ids = httpContext.Session.Get<List<int>>(cartKey);

            // if id collection is null
            ids ??= new List<int>();

            ids.Add(id);

            // save product to cart
            httpContext.Session.Set(cartKey, ids);
        }

        public void Clear()
        {
            // TODO
            throw new NotImplementedException();
        }

        public List<Product?> GetAll()
        {
            // get all products in the cart
            var ids = httpContext.Session.Get<List<int>>(cartKey);
            ids ??= new List<int>();

            return ids.Select(id => context.Products.Find(id)).ToList();
        }

        public int GetCount()
        {
            var ids = httpContext.Session.Get<List<int>>(cartKey);
            return ids?.Count ?? 0;
        }

        public void Remove(int id)
        {
            List<int>? ids = httpContext.Session.Get<List<int>>(cartKey);

            if (ids != null)
            {
                ids.Remove(id);
                // save product to cart
                httpContext.Session.Set(cartKey, ids);
            }
        }
    }
}
