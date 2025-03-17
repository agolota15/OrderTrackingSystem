using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace OrderTrackingSystem.Web.Hubs
{
    public class OrderHub : Hub
    {
<<<<<<< HEAD
        // Metoda wywoływana przez klienta, aby rozesłać powiadomienie
=======
>>>>>>> e9e9ea3f3becd2184cf5789cf802855666d746a5
        public async Task SendOrderUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveOrderUpdate", message);
        }
    }
}
