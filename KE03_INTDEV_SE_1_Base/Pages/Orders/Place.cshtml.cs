using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages.Orders
{
    public class PlaceModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;

        [BindProperty] public int CustomerId { get; set; }
        [BindProperty] public Dictionary<int, int> ProductQuantities { get; set; } = new();


        public Customer? Customer { get; set; }
        public IEnumerable<Product> Products { get; set; } = [];
        public PlaceModel(IOrderRepository orderRepository, IProductRepository productRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
        }

        public void OnGet(int customerId) { 
            CustomerId = customerId;
            Customer = _customerRepository.GetCustomerById(customerId);
            Products = _productRepository.GetAllProducts();
        }

        public IActionResult OnPost()
        {
            var order = new Order
            {
                CustomerId = CustomerId,
                OrderDate = DateTime.Now
            };
            foreach (var (productId, quantity) in ProductQuantities)
            {
                var product = _productRepository.GetProductById(productId);
                if (product != null)
                {
                    for (int i = 0; i < quantity; i++)
                    {
                        order.Products.Add(product);
                    }
                }
            }
            _orderRepository.AddOrder(order);
            return RedirectToPage("/Orders/Confirmation", new { orderId = order.Id });
        }
    }
}
