using Desire_And_Doom.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS.Components
{
    class Entity_Particle_Emitter : Component
    {
        public Vector2 Offset { get; set; } = Vector2.Zero;
        public Particle_Emitter Emitter { get; set; }

        public float Offset_X { get => Offset.X; }
        public float Offset_Y { get => Offset.Y; }

        public Particle_World World { get; set; }

        public bool Active { get; set; } = true;

        public Entity_Particle_Emitter(Particle_Emitter emitter, Particle_World world, bool active = true) : base(Types.Entity_Particle_Emitter)
        {
            Emitter = emitter;
            World = world;
            Active = active;

            world.Add(emitter);
        }
    }
}
