using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Desire_And_Doom.ECS;

namespace Desire_And_Doom.Graphics.Particle_Systems
{
    class Fire_Particle : Particle
    {
        public Fire_Particle()
        {
            Image = Assets.It.Get<Texture2D>("entities");
            Region = new Rectangle(502, 82, 10, 10);
            var rnd = new Random();

            if ( rnd.Next() % 2 == 0 )
                Flip = SpriteEffects.FlipHorizontally;

            Rotation = rnd.Next() % 360;

            Scale = 1.2f;
            Transparency = 1;
            Life = 80/(1 + rnd.Next() % 80);
        }

        public override void Update(GameTime time)
        {
            Countdown_Life(time);
            Apply_Velocity(time);
            Fade_Out();
            Scale_Down(0.98f);
        }
    }
}
