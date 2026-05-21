using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IProductRepository _productRepository;

        public IList<Product> Products { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            Products = new List<Product>();
        }

        public void OnGet()
        {
            Products = _productRepository.GetAllProducts().ToList();
        }

        public IActionResult OnPostAddToCart(int productId, int quantity = 1)
        {
            var cart = HttpContext.Session.GetString("cart") ?? "";
            var items = cart == "" ? new List<string>() : cart.Split(',').ToList();
            for (int i = 0; i < quantity; i++)
            {
                items.Add(productId.ToString());
            }
            HttpContext.Session.SetString("cart", string.Join(",", items));
            return RedirectToPage("/Index");
        }
    }
}