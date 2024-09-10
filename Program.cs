using Lagrange.Core;
using System.Net.WebSockets;

namespace LeneRapraelBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AutoResetEvent reset = new(false);

            var (ws,uri)=new Utils.WsClient().GetWebsocketInfo();

            using (ws)
            {
                
            }


        }
    }
}
