using Microsoft.Xna.Framework;
using Penumbra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS
{
    class Light_Emitter : Component
    {
        public PointLight Light { get; set; }
        public Vector2 Offset { get; set; } = Vector2.Zero;

        public Light_Emitter(PenumbraComponent lighting, float scale = 300) : base(Types.Light_Emitter)
        {
            Light = new PointLight()
            {
                Scale = new Vector2(scale),
                Color = Color.AliceBlue,
                Intensity = 0.5f,
                Radius = 0.2f,
                ShadowType = ShadowType.Occluded
            };
            lighting.Lights.Add(Light);
        }
    }
}
