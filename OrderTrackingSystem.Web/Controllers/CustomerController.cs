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

        // Strona główna klienta – lista zamówień klienta
        public async Task<IActionResult> Index()
        {
            // Tutaj możesz filtrować zamówienia przypisane do zalogowanego klienta
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }

        // Akcja tworzenia nowego zamówienia przez klienta
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
                // Dodatkowo możesz ustawić właściciela zamówienia, np. na podstawie User.Identity.Name
                await _orderService.CreateOrderAsync(order);
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

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
    }
}
