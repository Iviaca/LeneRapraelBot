using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeneRapraelBot.Interface
{
    internal interface IWebSocket
    {
        public void Connect() { }
        public void DisConnect() { }
        public void Send(string content) {
            ArraySegment<byte> bufferArray = new ArraySegment<byte>();

        
        }
    }
}
