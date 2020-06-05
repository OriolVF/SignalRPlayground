using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRSandbox.Hubs
{
    public class Records : Dictionary<string, string>
    {

    }

    public class ChatHub : Hub
    {
        private readonly Records _records;
        public ChatHub(Records records)
        {
            _records = records;
        }

        public async Task SendMessage(string user, string message)
        {
            _records.Add(user, message);
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            foreach (var record in _records)
            {
                await Clients.Caller.SendAsync("ReceiveMessage", record.Key, record.Value);
            }
            await base.OnConnectedAsync();
        } 
    }
}
