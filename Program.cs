using Lagrange.Core;
using LeneRapraelBot.Communication;
using LeneRapraelBot.Utils;
using Newtonsoft.Json;
using System.Collections.Frozen;
using System.Net.WebSockets;

namespace LeneRapraelBot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AutoResetEvent reset = new(false);

            WsClient ws = new();

            ws.OnReceive += (s, e) =>
            {
                Console.WriteLine(e);
            };

            ws.Connect();

            ws.WaitInputSend();

            Dictionary<string, string> _params = new();
            _params.Add("group_id", "xxxxxxxx");
            _params.Add("message", "可爱捏");
            _params.Add("auto_escape", "true");
            var pa = _params.ToFrozenDictionary();

            Post postRequest = new Post("send_group_msg", pa, "ehehooo");

            string json = JsonConvert.SerializeObject(postRequest.GetActionPost());
            Console.WriteLine(json);
            ws.SendMsg(json);


            reset.WaitOne();
        }
    }
}
