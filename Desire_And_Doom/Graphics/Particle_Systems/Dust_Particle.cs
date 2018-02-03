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

            var rnd = new Random();

            if ( rnd.Next() % 2 == 0 )
                Flip = SpriteEffects.FlipHorizontally;

            Rotation = rnd.Next() % 360;

            Scale = (float)(0.4f);
            Life = (float)(0.1f + rnd.NextDouble() * 0.5f);

            //Color = new Color(
            //    0.376f * 1.1f,
            //    0.356f * 1.1f,
            //    0.549f * 1.1f, 0.5f);
            Transparency = 10f;
        }

        public override void Update(GameTime time)
        {
            Countdown_Life(time);
            Apply_Velocity(time);
            Fade_Out();
            Scale_Down(0.99f);
        }
    }
}
