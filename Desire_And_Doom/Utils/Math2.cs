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
    }
}
