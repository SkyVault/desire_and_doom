using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.Graphics
{
    class Particle_World
    {
        List<Particle_Emitter> emitters;
        public Particle_World()
        {
            emitters = new List<Particle_Emitter>();
        }

        public void Add(Particle_Emitter emitter)
        {
            emitters.Add(emitter);
        }

        public void Update(GameTime time)
        {
            for (int i = emitters.Count - 1; i >= 0; i--)
            {
                var emitter = emitters[i];
                if (emitter.Remove)
                {
                    emitters.Remove(emitter);
                }else
                {
                    emitter.Update(time);
                }
            }
        }

        public void Destroy()
        {
            emitters.Clear();
        }

        public void Draw(SpriteBatch batch)
        {
            emitters.ForEach(e => e.Draw(batch));
        }
    }
}
