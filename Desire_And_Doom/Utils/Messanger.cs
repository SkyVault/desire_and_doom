using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.Utils
{
    class Messanger
    {
        private static Messanger it;
        private List<string> messages;

        private Messanger() {
            messages = new List<string>();
        }

        public void Push(string m)
        {
            messages.Add(m);
        }

        public string Top()
        {
            if (messages.Count > 0)
                return messages.Last();
            else return "";
        }

        public void Clear()
        {
            messages.Clear();
        }

        public static Messanger It
        {
            get
            {
                if (it == null) it = new Messanger();
                return it;
            }
        }
    }
}
