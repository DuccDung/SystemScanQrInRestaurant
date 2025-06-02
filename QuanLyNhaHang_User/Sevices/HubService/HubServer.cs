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
        public async Task SendMessageInMenuIncreasedProduct(string userId, string message)
        {
            await Clients.User(userId).SendAsync("UpdateIncreasedOrderDetailView", message);
        }
        public async Task SendMessageInMenuDecreasedProduct(string userId, string message)
        {
            await Clients.User(userId).SendAsync("UpdateDecreasedOrderDetailView", message);
        }
        public async Task SendMessageInMenuRemoveOrderDetail(string userId, string message)
        {
            await Clients.User(userId).SendAsync("UpdateRemoveOrderDetailView", message);
        }
        public async Task SendMessageToAll(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
