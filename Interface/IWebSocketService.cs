using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace LeneRapraelBot.Interface
{
    public interface IWebSocketService
    {
        public void Connect();
        public void Disconnect();
        public void Send(ClientWebSocket ws, string content);
        public string Receive(ClientWebSocket ws);
    }
}
