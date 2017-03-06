using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Desire_And_Doom.ECS
{
    class Sprite_Renderer_System : System
    {
        public Sprite_Renderer_System() : base(typeof(Body), typeof(Sprite))
        {
        }

        public override void Draw(SpriteBatch batch, Entity entity)
        {
            var sprite = (Sprite)entity.Get<Sprite>();
            var body = (Body)entity.Get<Body>();
            batch.Draw(sprite.Texture, body.Position + sprite.Offset, sprite.Quad, sprite.Color);
        }
    }
}
