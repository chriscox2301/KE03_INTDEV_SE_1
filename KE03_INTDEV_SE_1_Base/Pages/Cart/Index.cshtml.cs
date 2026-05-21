using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using DataAccessLayer.Models;

namespace KE03_INTDEV_SE_1_Base.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        public List<Product> CartItems { get; set; } = new();

        public IndexModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public void OnGet()
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != "")
            {
                var ids = cart.Split(',').Select(int.Parse).ToList();
                CartItems = ids
                    .Select(id => _productRepository.GetProductById(id))
                    .Where(p => p != null)
                    .ToList()!;
            }
        }

        public IActionResult OnPostRemove(int ProductId)
        {
            var cart = HttpContext.Session.GetString("cart") ?? "";
            var items = cart.Split(',').ToList();
            items.Remove(ProductId.ToString());
            HttpContext.Session.SetString("cart", string.Join(",", items));
            return RedirectToPage();
        }
    }
}
