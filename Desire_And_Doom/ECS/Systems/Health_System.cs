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
    class Health_System : System
    {
        public Health_System() : base(Types.Health)
        {
        }

        public override void Update(GameTime time, Entity entity)
        {
            base.Update(time, entity);

            var health = (Health)entity.Get(Types.Health);
            if (health.Shield_Timer >= 0)
            {
                health.Shield_Timer -= (float)time.ElapsedGameTime.TotalSeconds;
            }
        }
    }
}
