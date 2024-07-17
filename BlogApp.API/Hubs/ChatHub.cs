using BlogApp.API.Hubs.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace BlogApp.API.Hubs
{
    public sealed class ChatHub : Hub<IChatClient>
    {
        public async override Task OnConnectedAsync()
        {
            await Clients.All.ReceiveMessage($"{Context.ConnectionId} sohbete qatildi");
        }
        public async Task SendMessage(string message)
        {
            await Clients.All.ReceiveMessage( $"{Context.ConnectionId}: {message}");
        }
    }
}
