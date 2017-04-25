using Desire_And_Doom.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using CColor = System.Drawing.Color;

namespace Desire_And_Doom
{
    class Dialog_Word
    {
        public string Text { get; set; } = "";
        public Color Color { get; set; } = Color.White;
    }

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

        public bool Show_Portait { get; set; } = false;
        public Texture2D Image { get; set; }
        public Rectangle Region { get; set; }

        private Color color = Color.White;
        private List<Dialog_Word> words;

        private Vector2 position_offset { get; set; }

        public Dialog_Box()
        {
            position_offset = new Vector2(0, Height * 8 * Scale);
        }

        public void Animate_Toggle(string text)
        {
            animating   = !animating;

            if (animating == false )
            {
                position_offset += new Vector2(0, Height * 8 * Scale);
            }

            char_ptr    = 1;
            Text        = text;
            timer       = 0;
            words       = Compile_To_Words(text);

            Width = (Game1.WIDTH / (int)(8 * Scale));

            Position = new Vector2(
                0,
                Game1.HEIGHT - (Height * 8) * Scale
                ); 

            if (animating)
                Messanger.It.Push("Dialog_Open");
            else
                Messanger.It.Push("Dialog_Closed");
        }

        private List<Dialog_Word> Compile_To_Words(string text)
        {
            var list = new List<Dialog_Word>();

            var tokens = text.Split(' ', '!', '\n');
            
            return list;
        }

        public void Stop()
        {
            animating = false;
        }

        public bool Showing() => animating;

        public void Update(GameTime time)
        {
            if ( animating )
            {
                position_offset *= 0.98f;
            }
        }
        
        public void Draw(SpriteBatch batch, Camera_2D camera)
        {
            if (animating)
            {
                var image = Assets.It.Get<Texture2D>("gui");
                var font = Assets.It.Get<SpriteFont>("font");

                var pos = new Vector2(10, 10);

                var pre_width = Width;

                //move and shrink the dialog box to fit the portait
                if ( Show_Portait )
                {
                    Width -= Height;
                    Position = new Vector2(8*Height*Scale, Position.Y);
                }

                void Draw_Box(float xoff, float yoff, int w, int h)
                {
                    var pre_pos = Position;
                    Position = new Vector2(xoff, yoff) + position_offset;
                    for(int y = 0; y < h; y++)
                        for (int x = 0; x < w; x++)
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
                    Position = pre_pos;
                }

                Draw_Box(Position.X, Position.Y, Width, Height);

                Width = pre_width;

                //batch.Draw(image, pos, new Rectangle(0, 0, 64, 64), Color.Red, 0, Vector2.Zero,new Vector2(16, 3 * 4), SpriteEffects.None, 1f);
                var position = Position + new Vector2(20, 20);

                var text = Text;
                var tokens = text.Split(' ');

                if (tokens.Length > 1)
                {
                    float x = position.X;
                    float y = position.Y;
                    foreach ( var token in tokens )
                    {
                        if (token.Length > 0 && token.First() == '#' )
                        {
                            var _color = token.Split('#').Last();
                            var tmp = CColor.FromName(_color);
                            color = new Color(tmp.R, tmp.G, tmp.B, tmp.A);
                        }
                        else
                        {
                            batch.DrawString(font, token, new Vector2(x, y) + position_offset, color);
                            x += (font.MeasureString(token).X + font.MeasureString(" ").X);
                        }

                        if ( token.Contains("\n")) {
                            y += (font.MeasureString(" ").Y);
                            x = position.X;
                        }

                    }
                }

                if ( Show_Portait )
                {
                    Draw_Box(0, Position.Y, 6, 6);
                    if ( Region != null && Image != null )
                    {
                        float rw = Region.Width;
                        float rh = Region.Height;
                        float aspect = (rw / rh);
                        batch.Draw(
                            Image, 
                            new Rectangle(
                                (int)(Scale * (rw / Width) + position_offset.X), 
                                (int) (Position.Y + position_offset.Y), 
                                (int)(8 * (Height * aspect) * Scale), 
                                (int)(8 * Height * Scale)), 
                            Region, 
                            Color.White,
                            0, Vector2.Zero, SpriteEffects.None, 1);
                    }
                }
            }
        }
    }
}
