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

<<<<<<< HEAD
        // Lista zamówień klienta (można filtrować po aktualnie zalogowanym użytkowniku)
        public async Task<IActionResult> Index()
        {
=======
        // Strona główna klienta – lista zamówień klienta
        public async Task<IActionResult> Index()
        {
            // Tutaj możesz filtrować zamówienia przypisane do zalogowanego klienta
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }

<<<<<<< HEAD
        // Tworzenie zamówienia przez klienta
=======
        // Akcja tworzenia nowego zamówienia przez klienta
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
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
<<<<<<< HEAD
                // Przykładowo ustaw właściciela zamówienia: order.CustomerName = User.Identity.Name;
=======
                // Dodatkowo możesz ustawić właściciela zamówienia, np. na podstawie User.Identity.Name
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
                await _orderService.CreateOrderAsync(order);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

<<<<<<< HEAD
        // Stuby dla zarządzania wiadomościami, reklamacjami, trackingiem i bonami
        public IActionResult ManageMessages() => View();
        public IActionResult ComplaintTemplates() => View();
        public IActionResult TrackOrder() => View();
        public IActionResult UseStoreVouchers() => View();
=======
        // Akcje zarządzania wiadomościami, wzorcami reklamacyjnymi, bonami sklepowymi – przykładowe stuby
        public IActionResult ManageMessages()
        {
            // Implementacja zarządzania wiadomościami
            return View();
        }

        public IActionResult ComplaintTemplates()
        {
            // Implementacja tworzenia wzorców reklamacyjnych
            return View();
        }

        public IActionResult TrackOrder()
        {
            // Implementacja pełnego trackingu zamówienia
            return View();
        }

        public IActionResult UseStoreVouchers()
        {
            // Implementacja korzystania z bonów sklepu
            return View();
        }
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
    }
}
