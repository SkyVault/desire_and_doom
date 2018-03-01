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

        public void DrawLineRect(Vector2 position, Vector2 size, Color color, int line_width = 2, float layer = 1.0f)
        {
            // NOTE: Rotation takes some trig, so we'll deal with that later .... 

            //Left Line
            DrawFilledRect(
                position - new Vector2(line_width) / 2, 
                new Vector2(line_width, size.Y), 
                color, 
                0.0f, 
                layer);

            //Top line
            DrawFilledRect(
                position - new Vector2(line_width) / 2, 
                new Vector2(size.X, line_width), 
                color, 
                0.0f, 
                layer);

            //Left Line
            DrawFilledRect(
                position + new Vector2(size.X, 0) - new Vector2(line_width) / 2, 
                new Vector2(line_width, size.Y + line_width), 
                color, 
                0.0f, 
                layer);

            //Top line
            DrawFilledRect(
                position + new Vector2(0, size.Y) - new Vector2(line_width) / 2, 
                new Vector2(size.X + line_width, line_width), 
                color, 
                0.0f, 
                layer);
        }
    }
}
