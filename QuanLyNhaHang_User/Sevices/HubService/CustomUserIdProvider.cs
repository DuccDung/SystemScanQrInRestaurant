using Microsoft.AspNetCore.SignalR;

namespace QuanLyNhaHang_User.Sevices.HubService
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            var userId = connection.GetHttpContext()?.Request.Cookies["userId"];
            Console.WriteLine("[SignalR] Cookie userId = " + userId);
            return userId;
        }
    }
}
