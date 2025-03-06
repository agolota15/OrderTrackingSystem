using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderTrackingSystem.Domain.Models;
using OrderTrackingSystem.Services.Interfaces;
using System.Threading.Tasks;

namespace OrderTrackingSystem.Web.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private readonly IOrderService _orderService;
        public CustomerController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Lista zamówień klienta (można filtrować po aktualnie zalogowanym użytkowniku)
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }

        // Tworzenie zamówienia przez klienta
        public IActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                // Przykładowo ustaw właściciela zamówienia: order.CustomerName = User.Identity.Name;
                await _orderService.CreateOrderAsync(order);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // Stuby dla zarządzania wiadomościami, reklamacjami, trackingiem i bonami
        public IActionResult ManageMessages() => View();
        public IActionResult ComplaintTemplates() => View();
        public IActionResult TrackOrder() => View();
        public IActionResult UseStoreVouchers() => View();
    }
}
