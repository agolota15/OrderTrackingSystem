using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace OrderTrackingSystem.Web.Hubs
{
    public class OrderHub : Hub
    {
        public async Task SendOrderUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveOrderUpdate", message);
        }
    }
}
