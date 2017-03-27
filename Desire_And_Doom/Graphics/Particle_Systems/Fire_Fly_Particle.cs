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
    class Fire_Fly_Particle : Particle
    {
        public Fire_Fly_Particle()
        {
            Image = Assets.It.Get<Texture2D>("entities");
            Region = new Rectangle(482, 52, 30, 30);

            var rnd = new Random();
            Direction = Physics.Deg_To_Rad(rnd.Next() % 360);
            Scale = 0.05f;
            Transparency = 0;
            
        }

        public override void Update(GameTime time)
        {
            var rnd = new Random();
            Direction += Physics.Deg_To_Rad(-5 + rnd.Next() % 10);
            Countdown_Life(time);
            Move_In_Direction();
            Apply_Velocity(time);

            Fade_In();
            Fade_Out();
        }
    }
}
