using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace KE03_INTDEV_SE_1_Base.Pages.Cart
{
    public class CheckoutModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public List<Customer> Customers { get; set; }
        public List<Product> CartItems { get; set; } = new();

        [BindProperty] public int SelectedCustomerId { get; set; }

        public CheckoutModel(ICustomerRepository customerRepository, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }
        public void OnGet()
        {
            Customers = _customerRepository.GetAllCustomers().ToList();
            var cart = HttpContext.Session.GetString("cart") ?? "";
            if (cart != "")
            {
                var ids = cart.Split(',').Select(int.Parse).ToList();
                CartItems = ids
                    .Select(id => _productRepository.GetProductById(id))
                    .Where(p => p != null)
                    .ToList()!;
            }
        }

        public IActionResult OnPost()
        {
            var cart = HttpContext.Session.GetString("cart") ?? "";
            if (cart == "") return RedirectToPage("/Cart/Index");
            var ids = cart.Split(',').Select(int.Parse).ToList();

            var order = new Order
            {
                CustomerId = SelectedCustomerId,
                OrderDate = DateTime.Now
            };
            foreach (var group in ids.GroupBy(id => id))
            {
                var product = _productRepository.GetProductById(group.Key);
                if (product != null)
                {
                    order.OrderLines.Add(new OrderLine
                    {
                        ProductId = product.Id,
                        Quantity = group.Count(),
                        UnitPrice = product.Price
                    });
                }
            }
            _orderRepository.AddOrder(order);
            HttpContext.Session.Remove("cart");
            return RedirectToPage("/Orders/Confirmation", new { OrderId = order.Id });
        }
    }
}
