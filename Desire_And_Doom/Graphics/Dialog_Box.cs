using Desire_And_Doom.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom
{
    class Dialog_Box
    {
        public Vector2 Position { get; set; } = Vector2.Zero;
        public string Text { get; set; } = "";

        private bool animating  = false;
        private int char_ptr    = 1;
        private float timer     = 0;
        private float max_time  = 0.08f;

        public int Width { get; set; } = 10;
        public int Height { get; set; } = 6;
        public float Scale { get; set; } = 4f;

        private Color color = Color.White;

        public void Animate_Toggle(string text)
        {
            animating   = !animating;
            char_ptr    = 1;
            Text        = text;
            timer       = 0;

            if (animating)
                Messanger.It.Push("Dialog_Open");
            else
                Messanger.It.Push("Dialog_Closed");
        }

        public void Update(GameTime time)
        {
            if (animating)
            {
                timer += (float)time.ElapsedGameTime.TotalSeconds;
                if (timer > max_time) {
                    timer = 0;
                    char_ptr++;
                }
                if (char_ptr > Text.Length) char_ptr = Text.Length;
                //Position = new Vector2(Position.X + (float)time.ElapsedGameTime.TotalSeconds * 10, Position.X);
            }
        }

        public void Draw(SpriteBatch batch, Camera_2D camera)
        {
            if (animating)
            {
                var image = Assets.It.Get<Texture2D>("gui");
                var font = Assets.It.Get<SpriteFont>("font");

                var pos = new Vector2(10, 10);

                for(int y = 0; y < Height; y++)
                    for (int x = 0; x < Width; x++)
                    {
                        if (x == 0 && y == 0)
                            batch.Draw(image, Position, new Rectangle(0, 0, 8, 8), Color.White, 0, Vector2.Zero, new Vector2(Scale), SpriteEffects.None, 1f);
                        if (x > 0 && x < Width - 1 && y == 0)
                            batch.Draw(image, Position + new Vector2(x * 8 * Scale, y * 8 * Scale), new Rectangle(8, 0, 8, 8), Color.White, 0, Vector2.Zero,new Vector2(Scale), SpriteEffects.None, 1f);
                        if (x == Width - 1 && y == 0)
                            batch.Draw(image, Position + new Vector2(x * 8 * Scale, y * 8 * Scale), new Rectangle(16, 0, 8, 8), Color.White, 0, Vector2.Zero, new Vector2(Scale), SpriteEffects.None, 1f);
                        if (x == 0 && y > 0 && y < Height - 1)
                            batch.Draw(image, Position + new Vector2(x * 8 * Scale, y * 8 * Scale), new Rectangle(0, 8, 8, 8), Color.White, 0, Vector2.Zero, new Vector2(Scale), SpriteEffects.None, 1f);
                        if (x == Width - 1 && y > 0 && y < Height - 1)
                            batch.Draw(image, Position + new Vector2(x * 8 * Scale, y * 8 * Scale), new Rectangle(16, 8, 8, 8), Color.White, 0, Vector2.Zero, new Vector2(Scale), SpriteEffects.None, 1f);
                        if (x == 0 && y == Height - 1)
                            batch.Draw(image, Position + new Vector2(x * 8 * Scale, y * 8 * Scale), new Rectangle(0, 16, 8, 8), Color.White, 0, Vector2.Zero, new Vector2(Scale), SpriteEffects.None, 1f);
                        if (x == Width-1 && y == Height - 1)
                            batch.Draw(image, Position + new Vector2(x * 8 * Scale, y * 8 * Scale), new Rectangle(16, 16, 8, 8), Color.White, 0, Vector2.Zero, new Vector2(Scale), SpriteEffects.None, 1f);
                        if (x > 0 && x < Width - 1 && y == Height - 1)
                            batch.Draw(image, Position + new Vector2(x * 8 * Scale, y * 8 * Scale), new Rectangle(8, 16, 8, 8), Color.White, 0, Vector2.Zero, new Vector2(Scale), SpriteEffects.None, 1f);
                        if (x > 0 && x < Width - 1 && y > 0 && y < Height - 1)
                            batch.Draw(image, Position + new Vector2(x * 8 * Scale, y * 8 * Scale), new Rectangle(8, 8, 8, 8), Color.White, 0, Vector2.Zero, new Vector2(Scale), SpriteEffects.None, 1f);
                    }


                //batch.Draw(image, pos, new Rectangle(0, 0, 64, 64), Color.Red, 0, Vector2.Zero,new Vector2(16, 3 * 4), SpriteEffects.None, 1f);
                var position = Position + new Vector2(20, 20);

                var text = Text;
                var tokens = text.Split(' ');

                batch.DrawString(font, tokens[0], Position + new Vector2(20, 20), Color.White);

                if (tokens.Length > 1)
                {
                    var x = 0f;
                    for (int i = 1; i < tokens.Length; i++)
                    {
                        if (tokens[i].Length > 0)
                            if (tokens[i].First() == '<' && tokens[i].Last() == '>')
                            {
                                var color = tokens[i].Substring(1, tokens.Length - 2);
                                //var prop = typeof(Color).GetProperties(color);

                                x -= font.MeasureString(tokens[i + 1]).X + font.MeasureString(" ").X;
                            }
                            else {
                                //if (tokens[i - 1].First() != '<')
                                x += font.MeasureString(tokens[i-1] + " ").X;
                                batch.DrawString(font, tokens[i],new Vector2(x, 0) + Position + new Vector2(20, 20), Color.White);
                            }
                        //var pref
                    }
                }
            }
        }
    }
}
