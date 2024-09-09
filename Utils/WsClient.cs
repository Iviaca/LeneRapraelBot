using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace LeneRapraelBot.Utils
{
    public  class WsClient
    {
        log4net.ILog log = log4net.LogManager.GetLogger("ClientWesocketLogger");
        string? connectionString = ConfigurationManager.ConnectionStrings.ToString();

        ClientWebSocket Ws { get; set; }
        Uri ConnectUri {  get; set; }

        public Tuple<ClientWebSocket,Uri> GetWebsocketInfo()
        {
            return new(Ws, ConnectUri);
        }


        public WsClient()
        {
            if (connectionString is not null)
            {
                ConnectUri = new Uri(connectionString);
                Ws = new ClientWebSocket();
            }
            else
            {
                log!.Fatal("Connection String null in App.config" +
                    "(it is possible that you don't have an App.config at all)");
                throw new Exception("Connection string null");
            }
        }

        public async Task ConnAsync(Uri? uri)
        {
            if(uri is null)
            {
                Ws.ConnectAsync()
            }
        }
    }
}
