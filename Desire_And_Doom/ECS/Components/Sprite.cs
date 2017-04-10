using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS
{
    class Sprite : Component
    {
        public Texture2D Texture    { get; protected set; }
        public Rectangle Quad       { get; protected set; }

        public Color    Color  { get; set; } = Color.White;
        public Vector2  Offset { get; set; } = Vector2.Zero;
        public Vector2  Scale  { get; set; } = Vector2.One;
        public float    Layer  { get; set; } = 0.5f;
        public float    Layer_Offset { get; set; } = 0.0f;

        public Sprite(Texture2D _texture, Rectangle _quad) : base(Types.Sprite)
        {
            Texture = _texture;
            Quad    = _quad;
        }
    }
}
