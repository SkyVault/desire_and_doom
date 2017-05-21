using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.Utils
{
    class Named_Action_List
    {
        Dictionary<string,Action> actions;

        public Named_Action_List(Dictionary<string, Action> actions) {
            this.actions = actions;
        }

        public string[] Names {
            get => actions.Keys.ToArray();
        }

        public void Call(int index)
        {
            if (index >= 0 && index < actions.Count)
                actions.ElementAt(index).Value?.Invoke();
        }

        public Action this[int index]
        {
            get => actions.ElementAt(index).Value;
        }
    }
}
