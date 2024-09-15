using LeneRapraelBot.Struct;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeneRapraelBot.Communication
{
    internal class Post
    {
        ActionPost actionPost { get; set; }

        /// <summary>
        /// construct post struct
        /// </summary>
        /// <param name="_action">动作</param>
        /// <param name="_params">参数:冻结词典</param>
        /// <param name="_echo">返回字符串</param>
        public Post(string _action, FrozenDictionary<string, string> _params, string _echo = "dafault action echo")
        {
            actionPost = new ActionPost(_action, _params, _echo!);
        }

        public ActionPost GetActionPost()
        {
            return actionPost;
        }
    }
}
