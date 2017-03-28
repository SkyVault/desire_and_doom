using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS.Components
{
    class World_Interaction : Component
    {
        public Func<Entity, Entity, bool> Update    { get; set; }

        public string ID { get; set; } = "";
        public float X { get; set; } = 0;
        public float Y { get; set; } = 0;

        public World_Interaction(Func<Entity, Entity, bool> update) : base(Types.World_Interaction)
        {
            Update      = update;
        }
    }
}
