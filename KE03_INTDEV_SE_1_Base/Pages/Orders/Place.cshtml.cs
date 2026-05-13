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
        [BindProperty] public List<int> SelectedProductIds { get; set; }


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
            //TODO: maak een nieuwe Order aan
            //TODO: zoek de geselecteerde producten op via SelectedProductIds
            //TODO: voeg ze toe aan de order en sla op
            //TODO: redirect naar History pagina
            return Page();
        }
    }
}
