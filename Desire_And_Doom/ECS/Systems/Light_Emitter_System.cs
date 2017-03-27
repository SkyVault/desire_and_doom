using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Desire_And_Doom.ECS.Systems
{
    class Light_Emitter_System : System
    {
        public Light_Emitter_System() : base(Component.Types.Body, Component.Types.Light_Emitter)
        {
        }

        public override void Update(GameTime time, Entity entity)
        {
            base.Update(time, entity);

            var body = (Body)(entity.Get(Component.Types.Body));
            var light = (Light_Emitter)(entity.Get(Component.Types.Light_Emitter));

            light.Light.Position = body.Position + light.Offset;
        }
    }
}
