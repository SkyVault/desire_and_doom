using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static Desire_And_Doom.ECS.Component;

namespace Desire_And_Doom.ECS.Systems
{

    //TODO: refactor the properties into a seperate derrived class
    class World_Interaction_System : System
    {
        public World_Interaction_System() : base(Types.Body, Types.World_Interaction, Types.Physics)
        {
        }

        public override void Update(GameTime time, Entity entity)
        {
            base.Update(time, entity);

            //Console.WriteLine("hello world!");
        }
    }
}
