using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Desire_And_Doom.ECS.Component;

namespace Desire_And_Doom.ECS.Components
{
    class Spawner : Component
    {
        public List<string> Entities { get; set; }
        private World world;

        public Spawner(List<string> _entities, World _world) : base(Types.Spawner)
        {
            Entities = _entities;
            world = _world;
        }
        
        public void Do_Spawn(float X, float Y) {
            var rnd = new Random();
            foreach (var item in Entities)
            {
                int dx = -5 + rnd.Next() % 10;
                int dy = -5 + rnd.Next() % 10;

                var ent = world.Create_Entity(Assets.It.Get<LuaTable>(item));

                if (ent.Has(Types.Body))
                {
                    var body = (Body)ent.Get(Types.Body);
                    body.X = X;
                    body.Y = Y;
                }

                var physics = (Physics)ent.Get(Types.Physics);
                if (physics != null)
                {
                    float angle = (float)rnd.Next() % 360;

                    physics.Apply_Force(rnd.Next() % 150, -Physics.Deg_To_Rad(angle));
                }
            }
        }
    }
}
