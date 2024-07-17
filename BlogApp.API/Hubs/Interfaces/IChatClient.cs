namespace BlogApp.API.Hubs.Interfaces
{
    public interface IChatClient
    {
        Task ReceiveMessage(string message);
    }
}
