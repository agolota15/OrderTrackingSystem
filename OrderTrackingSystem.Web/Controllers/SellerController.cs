using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OrderTrackingSystem.Services.Interfaces;

namespace OrderTrackingSystem.Web.Controllers
{
    [Authorize(Roles = "Seller")]
    public class SellerController : Controller
    {
        private readonly IOrderService _orderService;
        public SellerController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Strona główna sprzedawcy – np. lista reklamacji lub przesyłek
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }

        public IActionResult ManageComplaints()
        {
            // Implementacja zarządzania reklamacjami
            return View();
        }

        public IActionResult TrackShipments()
        {
            // Implementacja kontroli przesyłek
            return View();
        }

        public IActionResult ManageCorrespondence()
        {
            // Implementacja zarządzania korespondencją
            return View();
        }

        public IActionResult GenerateVouchers()
        {
            // Implementacja generowania bonów dla klientów
            return View();
        }

        public IActionResult RunAutomaticProcesses()
        {
            // Implementacja uruchamiania automatycznych procesów
            return View();
        }
    }
}
