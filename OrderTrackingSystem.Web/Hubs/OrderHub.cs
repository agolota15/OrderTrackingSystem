using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace OrderTrackingSystem.Web.Hubs
{
    public class OrderHub : Hub
    {
        // Metoda wywoływana przez klienta, aby rozesłać powiadomienie
        public async Task SendOrderUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveOrderUpdate", message);
        }
    }
}
