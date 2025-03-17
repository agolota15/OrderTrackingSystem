using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using OrderTrackingSystem.Services.Interfaces;
using System.Threading.Tasks;
=======
using System.Threading.Tasks;
using OrderTrackingSystem.Services.Interfaces;
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5

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

<<<<<<< HEAD
        // Strona główna sprzedawcy – lista zamówień, reklamacji, przesyłek itp.
=======
        // Strona główna sprzedawcy – np. lista reklamacji lub przesyłek
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }

<<<<<<< HEAD
        public IActionResult ManageComplaints() => View();
        public IActionResult TrackShipments() => View();
        public IActionResult ManageCorrespondence() => View();
        public IActionResult GenerateVouchers() => View();
        public IActionResult RunAutomaticProcesses() => View();
=======
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
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
    }
}
