using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.Graphics
{
    abstract class Particle_Emitter
    {
        List<Particle> particles;

        public BlendState BlendState { get; set; } = BlendState.NonPremultiplied;

        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public float X { get { return Position.X; } set { Position = new Vector2(value, Position.Y); } }
        public float Y { get { return Position.Y; } set { Position = new Vector2(Position.X, value); } }

        public float Spawn_Time_Max { get; set; } = 0.5f;
        public float Spawn_Timer    { get; set; } = 0;

        public float Life_Max { get; set; } = -1;
        public float Life_Timer { get; set; } = 0;

        public bool Remove { get; set; } = false;
        public bool Active { get; set; } = true;

        public Particle_Emitter(Vector2 _position, bool spawn_right_off = false)
        {
            particles = new List<Particle>();
            Position = _position;
            if (spawn_right_off)
                Spawn();
        }

        public void Update(GameTime time)
        {
            for (int i = particles.Count - 1; i >= 0; i--)
            {
                var particle = particles[i];

                if (particle.Life <= 0)
                    particle.Destroy();

                if (particle.Remove) particles.Remove(particle);
                else
                    particle.Update(time);

                if (Life_Max > 0)
                {
                    Life_Timer += (float)time.ElapsedGameTime.TotalSeconds;
                    if (Life_Timer >= Spawn_Time_Max)
                        Remove = true;
                }
            }

            if ( Active )
            {
                Spawn_Timer += (float) time.ElapsedGameTime.TotalSeconds;
                if ( Spawn_Timer > Spawn_Time_Max )
                {
                    Spawn();
                    Spawn_Timer = 0;
                }
            }
        }

        public void Draw(SpriteBatch batch)
        {
            particles.ForEach(p => p.Draw(batch));
        }

        public void Add(Particle p)
        {
            particles.Add(p);
        }

        public void Destroy()
        {
            particles.Clear();
        }

        public abstract void Spawn();
    }
}
