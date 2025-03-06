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

        // Przykładowy dashboard z wykresem (dane przygotowywane w widoku lub poprzez ViewModel)
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }
    }
}
