using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Desire_And_Doom.Graphics
{
    class PrimitivesBatch
    {
        private SpriteBatch batch;
        private GraphicsDevice device;

        private Texture2D rect_pixel;

        public PrimitivesBatch(SpriteBatch _batch, GraphicsDevice _device)
        {
            batch = _batch;
            device = _device;

            rect_pixel = new Texture2D(device, 1, 1);
            Color[] data = new Color[1] { Color.White };
            rect_pixel.SetData(data);
        }

        public void DrawFilledRect(Vector2 position, Vector2 size, Color color, float rotation = 0.0f, float layer = 1.0f)
        {
            batch.Draw(
                rect_pixel, 
                position,
                null,
                color,
                rotation,
                Vector2.Zero,
                size,
                SpriteEffects.None,
                layer
                );
        }
    }
}
