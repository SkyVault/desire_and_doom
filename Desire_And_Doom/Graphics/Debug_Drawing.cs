using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.Graphics
{
    class Debug_Drawing
    {
        public static void Draw_Fill_Rect(SpriteBatch batch, float x, float y, float w, float h, Color color)
        {
            var image = Assets.It.Get<Texture2D>("gui");
            batch.Draw(image, new Rectangle((int) x, (int) y, (int) w, (int) h), new Rectangle(0, 0, 16, 16), color, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        public static void Draw_Line_Rect(SpriteBatch batch, float x, float y, float w, float h, Color color)
        {
            var thickness = 2;
            var image = Assets.It.Get<Texture2D>("gui");
            batch.Draw(image, new Rectangle((int) x, (int) y, thickness, (int) h), new Rectangle(24, 0, 16, 16), color, 0, Vector2.Zero, SpriteEffects.None, 1);
            batch.Draw(image, new Rectangle((int) (x + w - thickness), (int) y, thickness, (int) h), new Rectangle(24, 0, 16, 16), color, 0, Vector2.Zero, SpriteEffects.None, 1);
            batch.Draw(image, new Rectangle((int) x, (int) y, (int)w, thickness), new Rectangle(24, 0, 16, 16), color, 0, Vector2.Zero, SpriteEffects.None, 1);
            batch.Draw(image, new Rectangle((int) x, (int) (y + h - thickness), (int) w, thickness), new Rectangle(24, 0, 16, 16), color, 0, Vector2.Zero, SpriteEffects.None, 1);
        }
    }
}
