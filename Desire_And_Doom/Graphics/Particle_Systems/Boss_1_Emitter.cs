using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Desire_And_Doom.Graphics.Particle_Systems
{
    class Boss_1_Emitter : Particle_Emitter
    {
        public Boss_1_Emitter(Vector2 _position, bool spawn_right_off = false) : base(_position, spawn_right_off)
        {
            Spawn_Time_Max = 0.02f;
            this.BlendState = BlendState.Additive;
        }

        public override void Spawn()
        {
            var image = Assets.It.Get<Texture2D>("Boss_Texture");

            var rnd = new Random();

            Add(new Boss_1_Particle() {
                Position = this.Position,
                Image = image,
                Region = new Rectangle(104, 20, 24, 24),
                Life = 1f,
                Friction = Vector2.One,
                Velocity = new Vector2(0, 100),
                Color = new Color((float)rnd.NextDouble(), (float)rnd.NextDouble(), (float)rnd.NextDouble(), 1f)
            });
        }
    }
}
