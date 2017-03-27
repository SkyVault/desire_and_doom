using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using static Desire_And_Doom.ECS.Component;

namespace Desire_And_Doom.ECS
{
    class Body_Renderer_System : System
    {
        public Body_Renderer_System() : base(Types.Body)
        {
        }

        public override void Draw(SpriteBatch batch, Entity entity)
        {
            var body = (Body)entity.Get(Component.Types.Body);
            //batch.DrawRectangle(new RectangleF(body.Position, body.Size), Color.Red, 2);
        }
    }
}
