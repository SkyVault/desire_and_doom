using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS
{
    class Invatory : Component
    {
        public enum Render_Spot{ Left, Center, Right };

        Entity[,] slots;
        
        public int H { get => slots.GetLength(0); }
        public int W { get => slots.GetLength(1); }

        public int Money { get; set; } = 0;

        public Vector2 Offset { get; set; } = Vector2.Zero;
        private World world;
        private Entity parent;
        
        public Invatory(World world, Entity parent, int w, int h) :base(Types.Invatory) {
            slots = new Entity[w, h];
            this.world = world;
            this.parent = parent;
        }

        public void Drop_Item(int x, int y)
        {
            var entity = slots[y, x];
            if (entity == null) return;
            entity.Loaded = false;
            entity.Revive();
            slots[y, x] = null;

            Body body = (Body)parent.Get(Types.Body);
            if (body != null)
            {
                Body ibody = (Body)entity.Get(Types.Body);
                if (ibody != null)
                {
                    ibody.Position = body.Position;
                    Physics iphysics = (Physics)entity.Get(Types.Physics);
                    Item item = (Item)entity.Get(Types.Item);
                    item.Collect_Timer = 2f;
                    item.Can_Collect = false;
                    if (iphysics != null)
                    {
                        Random rnd = new Random();
                        iphysics.Apply_Force(rnd.Next() % 100,Physics.Deg_To_Rad(
                            rnd.Next() % 360
                            ));
                    }
                }
            }

            world.Add(entity);
        }

        public bool Has_Space()
        {
            for (int y = 0; y < slots.GetLength(0); y++)
                for (int x = 0; x < slots.GetLength(1); x++)
                    if (slots[y, x] == null)
                        return true;
            return false;
        }

        public void Add_Item(Entity e)
        {
            if ( e.Has_Tag("Coin") )
            {
                Money++;
                return;
            }

            for (int y = 0; y < slots.GetLength(0); y++) {
                for (int x = 0; x < slots.GetLength(1); x++) {
                    if (slots[y, x] == null) {
                        slots[y, x] = e;
                        return;
                    }
                }
            }
        }

        internal Entity Get(int y, int x)
        {
            return slots[y, x];
        }
    }
}
