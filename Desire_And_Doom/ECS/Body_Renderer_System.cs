using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Shapes;
using Microsoft.Xna.Framework;

namespace Desire_And_Doom.ECS
{
    class Body_Renderer_System : System
    {
        public Body_Renderer_System() : base(typeof(Body))
        {
        }

        public override void Draw(SpriteBatch batch, Entity entity)
        {
            var body = (Body)entity.Get<Body>();
            batch.DrawRectangle(new RectangleF(body.Position, body.Size), Color.Red, 2);
        }
    }
}
