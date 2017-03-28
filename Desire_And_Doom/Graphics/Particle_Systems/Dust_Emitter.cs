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
        public bool Spawn_Dust { get; set; } = false;

        public Dust_Emitter(Vector2 _position, bool spawn_right_off = false) : base(_position, spawn_right_off)
        {
            Position = _position;
        }

        public override void Spawn()
        {
            if (Spawn_Dust)
            {

                Add(new Dust_Particle() {
                    Position = this.Position,
                });

            }
        }
    }
}
