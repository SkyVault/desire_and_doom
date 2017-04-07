using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS.Components
{
    class Timed_Destroy : Component
    {
        public float Time_Left { get; set; } = 0f;

        public Timed_Destroy(float time) : base(Types.Timed_Destroy)
        {
            Time_Left = time;
        }
    }
}
