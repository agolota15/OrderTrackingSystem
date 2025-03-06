using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderTrackingSystem.Services.Interfaces;
using System.Threading.Tasks;

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

        // Strona główna sprzedawcy – lista zamówień, reklamacji, przesyłek itp.
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }

        public IActionResult ManageComplaints() => View();
        public IActionResult TrackShipments() => View();
        public IActionResult ManageCorrespondence() => View();
        public IActionResult GenerateVouchers() => View();
        public IActionResult RunAutomaticProcesses() => View();
    }
}
