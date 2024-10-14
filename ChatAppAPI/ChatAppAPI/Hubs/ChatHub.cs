using ChatAppAPI.DataService;
using ChatAppAPI.Models;
using Microsoft.AspNetCore.SignalR;


namespace ChatAppAPI.Hubs
{
    public class ChatHub : Hub
    {
        private readonly SharedDb _shared;

        public ChatHub(SharedDb shared) => _shared = shared;

        public async Task JoinChat(UserConnection conn)
        {
            await Clients.All.SendAsync(method: "ReceiveMessage", arg1: "admin", arg2: $"{conn.Username} has joined");//Task
        }

        public async Task JoinSpecificChatRoom(UserConnection conn)
        {
          
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName: conn.ChatRoom);

            _shared.connections[Context.ConnectionId] = conn;

             await Clients.Group(conn.ChatRoom).SendAsync(method: "JoinSpecificChatRoom", arg1: "admin", arg2:$"{conn.Username} has joined { conn.ChatRoom}");
        }

        public async Task SendMessage(string msg)
        {
            if(_shared.connections.TryGetValue(Context.ConnectionId,out UserConnection conn))
            {
                await Clients.Group(conn.ChatRoom)
                    .SendAsync("ReceiveSpecificMessage",conn.Username,msg);
            }
        }

    }
}
