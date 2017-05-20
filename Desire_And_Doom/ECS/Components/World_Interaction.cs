using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NLua;

namespace Desire_And_Doom.ECS.Components
{
    class World_Interaction : Component
    {
        public Func<Entity, Entity, bool> Update    { get; set; }
        public LuaFunction Lua_Update               { get; set; }

        public bool Constant_Update { get; set; } = false;

        public enum Update_Type
        {
            LUA,
            LAMBDA
        }

        public Update_Type UType;

        public string ID { get; set; } = "";
        public float X { get; set; } = 0;
        public float Y { get; set; } = 0;

        public World_Interaction(Func<Entity, Entity, bool> update) : base(Types.World_Interaction)
        {
            Update      = update;
            UType = Update_Type.LAMBDA;
        }

        public World_Interaction(LuaFunction update): base(Types.World_Interaction)
        {
            Lua_Update = update;
            UType = Update_Type.LUA;
        }
    }
}
