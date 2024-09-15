using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeneRapraelBot.Struct
{
    struct ActionPost
    {
        public string action;
        public FrozenDictionary<string, string> @params;
        public string echo;

        public ActionPost(string _action, FrozenDictionary<string, string> _params, string _echo)
        {
            @action = _action;
            @params = _params;
            @echo = _echo;
        }
    }
}
