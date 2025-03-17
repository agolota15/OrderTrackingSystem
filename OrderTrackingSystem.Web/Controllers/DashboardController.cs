using Microsoft.AspNetCore.Mvc;
using OrderTrackingSystem.Services.Interfaces;
using System.Threading.Tasks;

namespace OrderTrackingSystem.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IOrderService _orderService;
        public DashboardController(IOrderService orderService)
        {
            _orderService = orderService;
        }

<<<<<<< HEAD
        // Przykładowy dashboard z wykresem (dane przygotowywane w widoku lub poprzez ViewModel)
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrdersAsync();
=======
        // GET: /Dashboard
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            // Przygotuj dane do wykresów, np. liczba zamówień wg statusu
            // Możesz stworzyć widok model DashboardViewModel z danymi raportowymi
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
            return View(orders);
        }
    }
}
