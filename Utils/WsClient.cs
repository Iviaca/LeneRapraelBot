using LeneRapraelBot.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace LeneRapraelBot.Utils
{
    public class WsClient : IWebSocketService
    {
        //logger
        log4net.ILog log = log4net.LogManager.GetLogger("ClientWesocketLogger");

        //connection string
        string? connectionString = ConfigurationManager.AppSettings["ConnectionString"];

        //client and uri
        ClientWebSocket Ws { get; set; }
        Uri ConnectUri { get; set; }

        //events
        public event EventHandler<string> OnConnect;
        public event EventHandler<string> OnDisconnect;
        public event EventHandler<string> OnReceive;
        public event EventHandler<string> OnSend;

        public Tuple<ClientWebSocket, Uri> GetWebSocketInfo()
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

        public void Send(ClientWebSocket ws, string content)
        {
            ArraySegment<byte> bufferArray = new ArraySegment<byte>(Encoding.UTF8.GetBytes(content));//encoding contents to bytes
            Task sendTask = ws.SendAsync(bufferArray, WebSocketMessageType.Binary, true, CancellationToken.None);//TODO:先预设定为一个包，后期可能会改
            sendTask.Wait();
        }

        public string Receive(ClientWebSocket ws)
        {
            ArraySegment<byte> receivedBufferArray = new ArraySegment<byte>();
            Task receiveTask = ws.ReceiveAsync(receivedBufferArray, CancellationToken.None);
            receiveTask.Wait();//receive bytes

            string receiveTxt = Encoding.UTF8.GetString(receivedBufferArray);//decoding bytes
            return receiveTxt;
        }

        public void Connect()
        {
            Task connectTask = Ws.ConnectAsync(ConnectUri, CancellationToken.None);
            connectTask.Wait();
            Task.Run(() =>
            {
                while (true)
                {
                    //OnReceive(Ws, Receive(Ws));
                    string data = Receive(Ws);
                    log.Info($"\n{data}");
                }
            });
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="content">string</param>
        public void SendMsg(string content)
        {
            Send(Ws, content);
        }


        public void Disconnect()
        {
            throw new NotImplementedException();
        }


    }
}
