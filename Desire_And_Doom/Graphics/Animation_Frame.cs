using Desire_And_Doom.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.Graphics
{
    class Animation_Frame : RectangleF
    {
        public float Frame_Time { get; set; }

        public Animation_Frame(int x, int y, int w, int h, float time = 0.1f) : base(x, y, w, h)
        {
            Frame_Time = time;
        }

        public Animation_Frame(Vector2 pos, Vector2 size, float time = 0.1f) : base(pos.X, pos.Y, size.X, size.Y)
        {
            Frame_Time = time;
        }
    }
}
