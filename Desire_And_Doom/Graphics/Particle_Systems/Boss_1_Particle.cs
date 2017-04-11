using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Desire_And_Doom.Graphics.Particle_Systems
{
    class Boss_1_Particle : Particle
    {
        public override void Update(GameTime time)
        {
            Countdown_Life(time);
            Apply_Velocity(time);
            this.Scale_Down(0.99f);
        }
    }
}
