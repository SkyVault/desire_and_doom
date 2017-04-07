using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using static Desire_And_Doom.ECS.Component;
using MonoGame.Extended;

namespace Desire_And_Doom.ECS
{
    class Invatory_Renderer_System : System
    {
        public Invatory_Renderer_System() : base(Types.Invatory, Types.Body)
        {
        }

        public override void UIDraw(SpriteBatch batch, Camera_2D camera, Entity entity)
        {
            var invatory = (Invatory)entity.Get(Types.Invatory);
            var body = (Body)entity.Get(Types.Body);
            
            if (!invatory.Draw) return;

            var offset = new Vector2(256, 32);
            //if (invatory.Spot == Invatory.Render_Spot.Left)
            //    offset.X -= 50;

            var gui = (Texture2D) Assets.It.Get<Texture2D>("gui");

            for (int y = 0; y < invatory.H; y++)
                for (int x = 0; x < invatory.W; x++)
                {
                    var pos     = new Vector2(x * 16, y * 16) + offset;
                    var region  = new Rectangle(24, 0, 24, 24);
                    var size = 48;

                    batch.Draw(gui, new Rectangle((int)offset.X + size * x + (2 * x), (int)offset.Y + size * y + (2 * y), size, size), new Rectangle(24, 0, 24, 24), new Color(0, 0, 0, 100));
                }
        }
    }
}
