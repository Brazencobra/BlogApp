using Microsoft.AspNetCore.SignalR;

namespace BlogApp.Business.Hubs
{
    public sealed class ChatHub : Hub
    {

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveMessage",$"{Context.ConnectionId} sohbete qatildi");
        }
    }
}
