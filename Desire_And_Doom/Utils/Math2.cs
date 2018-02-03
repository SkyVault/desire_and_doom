using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.Utils
{
    class Math2
    {
        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        public static Color Lerp(Color a, Color b, float t)
        {
            return new Color(
                    Lerp(a.R / 255f, b.R / 255f, t),
                    Lerp(a.G / 255f, b.G / 255f, t),
                    Lerp(a.B / 255f, b.B / 255f, t),
                    Lerp(a.A / 255f, b.R / 255f, t)
                );
        }
    }
}
