using Desire_And_Doom.ECS.Components;
using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Desire_And_Doom.ECS.Component;

namespace Desire_And_Doom.ECS.Systems
{
    class Enemy_System : System
    {
        public Enemy_System() : base(Types.Enemy)
        {
        }

        public override void Destroy(Entity entity)
        {
            base.Destroy(entity);

            var enemy = (Enemy) entity.Get(Types.Enemy);
            var body = (Body) entity.Get(Types.Body);

            var rnd = new Random();
            foreach(var item in enemy.Drop_Items )
            {
                int dx = -5 + rnd.Next() % 10;
                int dy = -5 + rnd.Next() % 10;

                var ent = World_Ref.Create_Entity(
                    Assets.It.Get<LuaTable>(item),
                    body.X + body.Width / 2 + dx,
                    body.Y + body.Height / 2 + dy
                    );

                var physics = (Physics)ent.Get(Types.Physics);
                if (physics != null )
                {
                    float angle = (float) rnd.Next() % 360;
                    
                    physics.Apply_Force(rnd.Next() % 150,-Physics.Deg_To_Rad(angle));
                }
            }
        }
    }
}
