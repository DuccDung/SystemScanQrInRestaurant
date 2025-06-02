using Microsoft.AspNetCore.SignalR;

namespace QuanLyNhaHang_User.Sevices
{
    public class HubServer : Hub
    {
        public HubServer() { }
        public async Task SendMessage(string userId, string message)
        {
            await Clients.User(userId).SendAsync("UpdateOrderDetailView", message);
        }
        public async Task SendMessageToAll(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
