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

        Entity[,] Slots;
        public bool Draw { get; set; } = false;
        public Render_Spot Spot { get; set; } = Invatory.Render_Spot.Left;

        public int H { get => Slots.GetLength(0); }
        public int W { get => Slots.GetLength(1); }

        public Invatory(int w, int h) :base(Types.Invatory) {
            Slots = new Entity[w, h];
        }

        public void Add_Item(Entity e)
        {
            for (int y = 0; y < Slots.GetLength(0); y++) {
                for (int x = 0; x < Slots.GetLength(1); x++) {
                    if (Slots[y, x] == null) {
                        Slots[y, x] = e;
                        return;
                    }
                }
            }
        }

        internal Entity Get(int y, int x)
        {
            return Slots[y, x];
        }
    }
}
