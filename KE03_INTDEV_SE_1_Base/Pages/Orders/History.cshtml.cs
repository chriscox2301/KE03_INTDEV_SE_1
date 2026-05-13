using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace KE03_INTDEV_SE_1_Base.Pages.Orders
{
    public class HistoryModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public IEnumerable<Order> Orders { get; set; } = [];
        public Customer? Customer { get; set; }

        public HistoryModel(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public void OnGet(int customerId)
        {
            Customer = _customerRepository.GetCustomerById(customerId);
            Orders = _orderRepository.GetOrdersByCustomer(customerId);
        }
    }
}
