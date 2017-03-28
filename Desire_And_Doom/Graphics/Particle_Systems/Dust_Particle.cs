using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Desire_And_Doom.Graphics.Particle_Systems
{
    class Dust_Particle : Particle
    {
        public Dust_Particle()
        {
            Image = Assets.It.Get<Texture2D>("entities");
            Region = new Rectangle(466, 53, 16, 16);
            Life = 1;
        }

        public override void Update(GameTime time)
        {
            Countdown_Life(time);
        }
    }
}
