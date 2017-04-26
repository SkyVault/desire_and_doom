﻿using Microsoft.Xna.Framework;
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
        
        public Invatory(int w, int h) :base(Types.Invatory) {
            slots = new Entity[w, h];
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
