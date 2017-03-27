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
        LuaTable dialog;

        public Npc(LuaTable dialog) : base(Types.Npc)
        {
        }
    }
}
