using Desire_And_Doom.ECS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS
{
    class Physics : Component
    {
        public enum PType
        {
            STATIC, DYNAMIC, WATER, SPACE, WORLD_INTERACTION
        };

        public List<string> Blacklisted_Collision_Tags { get; set; } = new List<string>();

        public Func<Entity, Entity, bool> Callback = null;
        public PType Physics_Type { get; }

        public bool Handle_Collisions { get; set; } = true;
        public Entity Other { get; set; } = null;

        public Vector2 Velocity { get; set; }

        public float Vel_X { get { return Velocity.X; } set { Velocity = new Vector2(value, Velocity.Y); } }
        public float Vel_Y { get { return Velocity.Y; } set { Velocity = new Vector2(Velocity.X, value); } }

        public float Density  { get; set; } = 0.01f;
        public float Friction { get; set; } = 0.005f;
        
        public float Direction { get; set; } = 0f;
        public float Current_Speed { get; set; } = 0f;

        public bool Flying { get; set; } = false;

        public static float Deg_To_Rad(float deg) {
            return (float)Math.PI * deg / 180;
        }

        public static float Rad_To_Deg(float rad) {
            return rad * (180f / (float)Math.PI);
        }

        public void Apply_Force(float force, float dir_rad)
        {
            Vel_X += (float)Math.Cos(dir_rad) * force;
            Vel_Y += (float)Math.Sin(dir_rad) * force;
        }

        public Physics_Engine Engine { get; set; }

        public Physics(Vector2 _velocity, PType _physics_type = PType.STATIC) : base(Types.Physics)
        {
            Physics_Type = _physics_type;
            Velocity = new Vector2(0, 0);
        }

        public bool Contains_Blacklisted_Tag(List<string> tags)
        {
            foreach(var tag in tags )
                if ( Blacklisted_Collision_Tags.Contains(tag) ) return true;
            return false;
        }
    }
}
