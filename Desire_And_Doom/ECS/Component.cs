using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS
{
    abstract class Component {
        public enum Types
        {
            Player,
            Item,
            Physics,
            Sprite,
            Animation,
            Invatory,
            Body,
            AI,
            Npc,
            Light_Emitter,
            Entity_Particle_Emitter,
            World_Interaction,
            Lua_Function,
            Num_Of_Types,
        }

        public Types Type { get; protected set; }
        public Component(Types Type)
        {
            this.Type = Type;
        }
    }
}
