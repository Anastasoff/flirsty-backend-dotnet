using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Flirsty.WebApi.Hubs
{
    [HubName("chat")]
    public class ChatHub : Hub
    {
        [HubMethodName("sendMessage")]
        public void SendMessage(string message)
        {
            string msg = $"Connection Id: {Context.ConnectionId}; Message: {message}";
            Clients.All.newMessage(msg);
        }
    }
}