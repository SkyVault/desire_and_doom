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
    class Timed_Destroy_System : System
    {
        public Timed_Destroy_System() : base(Types.Timed_Destroy)
        {
        }

        public override void Update(GameTime time, Entity entity)
        {
            base.Update(time, entity);

            var timer = (Timed_Destroy) entity.Get(Types.Timed_Destroy);
            if ( timer.Time_Left <= 0 )
                entity.Destroy();
            else
                timer.Time_Left -= (float) time.ElapsedGameTime.TotalSeconds;
        }
    }
}
