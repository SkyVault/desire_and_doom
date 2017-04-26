using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS
{
    class Npc : Component
    {
        public Dialog_Box Dialog { get; private set; }

        public Npc(LuaTable dialog_table) : base(Types.Npc)
        {
            Dialog = new Dialog_Box();
        }
    }
}
