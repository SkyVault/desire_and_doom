using Desire_And_Doom.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS.Components
{
    class Multipart_Animation : Component
    {
        public Dictionary<string, Animated_Sprite> Animation_Components { get; set; }

        public Multipart_Animation() : base(Types.Multipart_Animation)
        {
            Animation_Components = new Dictionary<string, Animated_Sprite>();
        }

    }
}
