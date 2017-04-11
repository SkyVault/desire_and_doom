
using Desire_And_Doom.ECS.Components;
using Microsoft.Xna.Framework;
using static Desire_And_Doom.ECS.Component;

namespace Desire_And_Doom.ECS.Systems
{
    class Particle_Emitter_System : System
    {
        public Particle_Emitter_System() : base(Types.Body, Types.Entity_Particle_Emitter)
        {
        }

        public override void Destroy(Entity entity)
        {
            base.Destroy(entity);

            var emitter_component = (Entity_Particle_Emitter) entity.Get(Types.Entity_Particle_Emitter);
            emitter_component.Emitter.Destroy();    
        }

        public override void Update(GameTime time, Entity entity)
        {
            base.Update(time, entity);

            var emitter_component = (Entity_Particle_Emitter) entity.Get(Types.Entity_Particle_Emitter);
            var body = (Body) entity.Get(Types.Body);

            emitter_component.Emitter.Position  = body.Position + emitter_component.Offset;
            emitter_component.Emitter.Active    = emitter_component.Active;
        }
    }
}
