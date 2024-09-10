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
        //logger
        log4net.ILog log = log4net.LogManager.GetLogger("ClientWesocketLogger");

        //connection string
        string? connectionString = ConfigurationManager.ConnectionStrings.ToString();

        //client and uri
        ClientWebSocket Ws { get; set; }
        Uri ConnectUri {  get; set; }

        //events
        public event EventHandler<string> OnConnect;
        public event EventHandler<string> OnDisconnect;
        public event EventHandler<string> OnReceive;
        public event EventHandler<string> OnSend;

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

        public async Task ConnAsync()
        {
            if(uri is null)
            {
                await Ws.ConnectAsync()
            }
        }
    }
}
