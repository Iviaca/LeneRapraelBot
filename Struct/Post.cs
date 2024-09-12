using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeneRapraelBot.Struct
{
    struct Post
    {
        string @action;
        List<FrozenDictionary<string, string>> @params;
        string @echo;
    }
}
