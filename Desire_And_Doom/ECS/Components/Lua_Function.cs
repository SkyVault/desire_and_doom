using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS.Components
{
    class Lua_Function : Component
    {
        public LuaFunction Function { get; set; }

        public Lua_Function(LuaFunction _function) : base(Types.Lua_Function)
        {
            Function = _function;
        }
    }
}
