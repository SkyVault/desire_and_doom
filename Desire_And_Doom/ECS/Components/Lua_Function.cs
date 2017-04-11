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

        public string File_Name { get; set; } = "";
        public bool Auto_Reload { get; set; } = false;

        //public DateTime Current_Date_Time { get; set; }
        public DateTime Last_Date_Time { get; set; } = DateTime.MaxValue;

        public Lua_Function(LuaFunction _function, string file_name = "") : base(Types.Lua_Function)
        {
            Function = _function;
            File_Name = file_name;

            if ( file_name != "" )
                Auto_Reload = true;
        }
    }
}
