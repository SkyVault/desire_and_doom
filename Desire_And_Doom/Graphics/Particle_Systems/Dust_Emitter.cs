using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Desire_And_Doom.Graphics.Particle_Systems
{
    class Dust_Emitter : Particle_Emitter
    {
        public Dust_Emitter(Vector2 _position, bool spawn_right_off = false) : base(_position, spawn_right_off)
        {
            Position = _position;
            this.Spawn_Time_Max = 0.05f;
        }

        public override void Spawn()
        {
            var rnd = new Random();
            var vel = new Vector2(0, -(20 + rnd.Next() % 40));
            Add(new Dust_Particle() {
                Position = Position + new Vector2(3 + -2.0f + (float) rnd.NextDouble() * 4, 4 + (float) (-2.0 + rnd.NextDouble() * 4)),
                Velocity = vel
            });
        }
    }
}
