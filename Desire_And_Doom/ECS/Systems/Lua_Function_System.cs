using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static Desire_And_Doom.ECS.Component;
using Desire_And_Doom.ECS.Components;

namespace Desire_And_Doom.ECS.Systems
{
    class Lua_Function_System : System
    {

        public Lua_Function_System() : base(Types.Lua_Function)
        {
        }

        public World Get_World() => World_Ref;

        public Entity Get_Player()
        {
            var entities = World_Ref.Get_All_With_Component(Types.Player);
            if ( entities.Count > 0 )
                return entities.Last();
            return null;
        }

        public float Dist(float x1, float y1, float x2, float y2)
        {
            return (float) (Vector2.Distance(new Vector2(x1, y1), new Vector2(x2, y2)));
        }

        public void Face_Move_Dir(Entity e)
        {
            var sprite = (Animated_Sprite)e.Get(Types.Animation);
            var physics = (Physics) e.Get(Types.Physics);
            if (sprite != null && physics != null)
            {
                if ( physics.Velocity.X > 0 )
                    sprite.Scale = new Vector2(1, sprite.Scale.Y);
                else
                    sprite.Scale = new Vector2(-1, sprite.Scale.Y);
            }
        }

        public bool Entity_Within(string tag, float x, float y, float dist)
        {
            var other = World_Ref.Find_With_Tag(tag);
            if (other != null)
            {
                var o_body = (Body) other.Get(Types.Body);
                var adist = Vector2.Distance(o_body.Position, new Vector2(x, y));
                if ( adist < dist ) return true;
            }
            return false;
        }

        public Entity Get_With_Tag(string tag)
        {
            return World_Ref.Find_With_Tag(tag);
        }

        public void Track(Entity self, Entity other, float force)
        {
            var physics = (Physics) self.Get(Types.Physics);
            var body = (Body) self.Get(Types.Body);
            if (physics != null && body != null)
            {
                var o_body = (Body) other.Get(Types.Body);
                var dot = body.Angle_To_Other(o_body);
                physics.Apply_Force(force, dot);
            }
        }

        public Component Get_Component(Entity e, string type)
        {
            //var _type = Enum.Parse, type);
            bool worked = Enum.TryParse(type, out Types component_type);
            if ( worked )
            {
                return e.Get(component_type);
            } else
            {
                Console.WriteLine("Unknown component: " + type);
            }
            return null;
        }

        public override void Update(GameTime time, Entity entity)
        {
            base.Update(time, entity);

            var fn = (Lua_Function) entity.Get(Types.Lua_Function);
            fn.Function?.Call(entity, this);
        }
    }
}
