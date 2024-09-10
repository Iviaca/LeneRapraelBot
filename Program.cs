using Lagrange.Core;
using LeneRapraelBot.Utils;
using System.Net.WebSockets;

namespace LeneRapraelBot
{
    internal class Program
    {
        static void Main(string[] args)
        {

            WsClient ws = new();
            ws.Connect();

        }
    }
}
