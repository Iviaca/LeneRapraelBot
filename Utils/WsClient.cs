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
    internal class WsClient : IWebSocketService
    {
        //logger
        log4net.ILog log = log4net.LogManager.GetLogger("ClientWesocketLogger");

        //connection string
        string? connectionString = ConfigurationManager.AppSettings["ConnectionString"];
        //string? connectionString = ConfigurationManager.AppSettings["ConnectionStringTest"];//Debug

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

        public string Send(string content)
        {
            if (Ws.State == WebSocketState.Connecting)
            {
                ArraySegment<byte> bufferArray = new ArraySegment<byte>(Encoding.UTF8.GetBytes(content));//encoding contents to bytes
                Task sendTask = Ws.SendAsync(bufferArray, WebSocketMessageType.Binary, true, CancellationToken.None);//TODO:先预设定为一个包，后期可能会改
                sendTask.Wait();

                if (OnSend is not null)
                {
                    OnSend.Invoke(Ws, content);
                }

                return "sending request sent";
            }
            else
            {
                log.Error("Connection Lost, cannot send");
                return "Connection Lost, cannot send";
            }
        }

        public string Receive()
        {
            if (Ws.State == WebSocketState.Connecting)
            {
                byte[] buffer = new byte[2048 * 2];
                ArraySegment<byte> receivedBufferArray = new ArraySegment<byte>(buffer);
                Task receiveTask = Ws.ReceiveAsync(receivedBufferArray, CancellationToken.None);
                receiveTask.Wait();//receive bytes

                string receiveTxt = Encoding.UTF8.GetString(receivedBufferArray);//decoding bytes

                if (OnReceive is not null)
                {
                    OnReceive.Invoke(Ws, receiveTxt);
                }

                return receiveTxt;
            }
            else
            {
                log.Error("Connection Lost, cannot receive");
                return "Connection Lost, cannot receive";
            }
        }

        public void Connect()
        {
            Task connectTask = Ws.ConnectAsync(ConnectUri, CancellationToken.None);
            connectTask.Wait();
            Task.Run(() =>
            {
                while (true)
                {
                    string data = Receive();
                }
            });

            if (OnConnect is not null)
            {
                OnConnect.Invoke(Ws, "connection established");
            }
        }

        public void Disconnect()
        {
            Task outputCloseTask = Ws.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Normally closed", CancellationToken.None);
            outputCloseTask.Wait();
            Task disconnectTask = Ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Normally closed", CancellationToken.None);
            disconnectTask.Wait();

            if (OnDisconnect is not null)
            {
                OnDisconnect.Invoke(Ws, "connection disconnected");
            }
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="content">string</param>
        public void SendMsg(string content)
        {
            Send(content);
        }

        public void WaitInputSend()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    Console.WriteLine("Waiting Input");
                    string? content = Console.ReadLine();

                    SendMsg(content);

                    string? str = Console.ReadLine();

                    int i = 0;

                    if (int.TryParse(str, out i) && i == 1)
                    {
                        OnDisconnect += (s, e) =>
                        {
                            Console.WriteLine(e);
                        };
                        Disconnect();
                    }
                }
            });
        }
    }
}
