using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Desire_And_Doom.ECS.Components;
using static Desire_And_Doom.ECS.Component;

namespace Desire_And_Doom.ECS.Systems
{
    class Entity_Emitter_System : System
    {
        public Entity_Emitter_System() : base(Types.Entity_Particle_Emitter, Types.Body)
        {
        }

        public override void Update(GameTime time, Entity entity)
        {
            base.Update(time, entity);

            var emitter = (Entity_Particle_Emitter)entity.Get(Types.Entity_Particle_Emitter);
            var body    = (Body)entity.Get(Types.Body);

            emitter.Emitter.Position = body.Position + emitter.Offset;

            if (emitter.Active)
                emitter.Emitter.Update(time);
        }
    }
}
