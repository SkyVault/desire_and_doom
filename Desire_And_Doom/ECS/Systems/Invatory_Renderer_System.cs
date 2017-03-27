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

            var offset = new Vector2(0, 0);
            if (invatory.Spot == Invatory.Render_Spot.Left)
                offset.X -= 50;

            for (int y = 0; y < invatory.H; y++)
                for (int x = 0; x < invatory.W; x++)
                {
                    var pos = new Vector2(x * 16, y * 16) + offset;
                    var off = new Vector2((invatory.W * 16) / 2, (invatory.H * 16) / 2);
                    //batch.DrawRectangle(body.Position + pos - off, new Vector2(16, 16), Color.White);

                    if (invatory.Get(y, x) != null)
                    {
                        Entity item = invatory.Get(y, x);
                        var sprite = (Sprite)item.Get(Types.Sprite);

                        batch.Draw(
                            sprite.Texture,
                            pos - off,
                            sprite.Quad, Color.White
                            );
                    }
                }
        }
    }
}
