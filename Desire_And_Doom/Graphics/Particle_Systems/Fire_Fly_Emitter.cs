using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Desire_And_Doom.Graphics.Particle_Systems
{
    class Fire_Fly_Emitter : Particle_Emitter
    {
        public Fire_Fly_Emitter(Vector2 _position) : base(_position, true)
        {
            Spawn_Time_Max = 0.01f;
        }

        public override void Spawn()
        {
            var rnd = new Random();
            var scale = 1000;
            Add(new Fire_Fly_Particle() { Position = new Vector2(rnd.Next() % scale, rnd.Next() % scale) });
        }
    }
}
