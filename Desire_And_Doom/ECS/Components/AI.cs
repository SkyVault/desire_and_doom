using Microsoft.Xna.Framework;
using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS
{
    class AI : Component
    {
        public LuaTable Behavior { get; }
        public float Timer { get; set; } = 0;
        public float Target_Angle { get; set; } = 0;

        public List<bool> Flags { get; set; }

        public bool TargetEntity_AStar { get; set; } = false;
        public Entity Target;

        public AI(Entity target) : base(Types.AI)
        {
            Target = target;
        }

        public AI(LuaTable behavior) : base(Types.AI)
        {
            Behavior = behavior;
            Flags = new List<bool>();
            for (int i = 0; i < 16; i++)
                Flags.Add(false);
        }
    }
}
