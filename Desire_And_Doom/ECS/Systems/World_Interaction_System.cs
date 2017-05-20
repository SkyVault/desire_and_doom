using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static Desire_And_Doom.ECS.Component;
using Desire_And_Doom.ECS.Components;

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

            var wi = (World_Interaction)entity.Get(Types.World_Interaction);
            if (wi.Constant_Update)
            {
                var o_interaction = (World_Interaction)entity.Get(Types.World_Interaction);
                if (o_interaction.UType == World_Interaction.Update_Type.LAMBDA)
                    o_interaction?.Update(entity, entity);
                else
                    o_interaction?.Lua_Update.Call(entity, entity);
            }
            //Console.WriteLine("hello world!");
        }
    }
}
