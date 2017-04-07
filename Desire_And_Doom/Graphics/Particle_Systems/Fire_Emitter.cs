using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Desire_And_Doom.Graphics.Particle_Systems
{
    class Fire_Emitter : Particle_Emitter
    {
        public Fire_Emitter(Vector2 _position, bool create_fire_on_creation = true) : base(_position, create_fire_on_creation)
        {
            Spawn_Time_Max = 0.02f;
            Position = _position;
            this.BlendState = Microsoft.Xna.Framework.Graphics.BlendState.Additive;
        }

        public override void Spawn()
        {
            var rnd = new Random();
            var vel = new Vector2(0, -(10 + rnd.Next() % 18));
            Add(new Fire_Particle() {
                Position = Position + new Vector2(3 + -2.0f + (float)rnd.NextDouble() * 4, 4 + (float)(-2.0 + rnd.NextDouble() * 4)),
                Velocity = vel
            });
        }
    }
}
