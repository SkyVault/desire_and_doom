using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input.InputListeners;
using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.Utils
{
    class Lua_Commander
    {
        public Lua_Commander()
        {

        }

        public void Exit() { Game1.SHOULD_QUIT = true; }
        //public void Spawn() { }
    }

    class Debug_Console : GameComponent
    {
        Rectangle region = new Rectangle(24, 0, 24, 24);

        enum State
        {
            CLOSED,
            HALF,
            QUARTER
        }

        State state = State.CLOSED;

        public static bool Open { get; private set; } = false;

        List<string> history;
        string text = "";

        Vector2 cursor = Vector2.Zero;
        Lua lua;

        public Debug_Console(Game game, Lua lua) : base(game)
        {
            history = new List<string>();
            this.lua = lua;

            lua["Version"] = "0.0.1";

            var commander = new Lua_Commander();
            lua["x"] = commander;
        }

        public void Key_Typed(object sender, KeyboardEventArgs args)
        {
            if (args.Key == Keys.Back)
            {
                if (text.Length > 0)
                {
                    var left = text.Substring(0, (int)cursor.X);
                    var right = text.Remove(0, (int)cursor.X);
                    left = left.Substring(0, left.Length - 1);
                    text = left + right;
                    cursor.X--;
                }
            }
            else if (args.Key == Keys.Enter)
            {
                // handle input
                try
                {
                    lua.DoString(text);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                history.Add(text);

                text = "";
            }
            else if (!(args.Key == Keys.OemTilde && args.Modifiers != KeyboardModifiers.Shift) && args.Key != Keys.Tab)
            {
                if (text.Length > 0)
                    text = text.Insert((int)cursor.X, args.Character?.ToString() ?? "");
                else text += args.Character?.ToString() ?? "";
                cursor.X++;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Input.It.Is_Key_Pressed(Keys.Left))
                cursor.X--;
            if (Input.It.Is_Key_Pressed(Keys.Right))
                cursor.X++;

            if (cursor.X > text.Length) cursor.X = text.Length;
            if (cursor.X < 0) cursor.X = 0;

            if (Input.It.Is_Key_Pressed(Keys.Up))
            {
                if (history.Count > 0)
                {
                    text = history.Last();
                    cursor.X = text.Length;
                    history.Remove(history.Last());
                }
            }

            if (Input.It.Is_Key_Pressed(Keys.OemTilde) && !Input.It.Is_Key_Down(Keys.LeftShift))
            {
                if (state == State.CLOSED)
                    state = State.QUARTER;
                else if (state == State.QUARTER)
                    state = State.HALF;
                else if (state == State.HALF)
                    state = State.CLOSED;

                if (state != State.CLOSED)
                {
                    Open = true;
                }
                else {
                    Open = false;
                }
            }

            
        }

        public void Draw(SpriteBatch batch)
        {
            if (state != State.CLOSED)
            {
                var image = Assets.It.Get<Texture2D>("gui");

                var height      = Game1.HEIGHT * (state == State.HALF ? 0.7f : 0.15f);
                var font        = Assets.It.Get<SpriteFont>("font");
                var str_height  = (int)font.MeasureString(" ").Y;
                var str_width   = (int)font.MeasureString(">").X;

                batch.Draw(image, new Rectangle(0, 0, Game1.WIDTH, (int)height), new Rectangle(24, 0, 24, 24), new Color(10, 10, 10, 150));
                batch.Draw(image, new Rectangle(0, (int)height - str_height, Game1.WIDTH, str_height), new Rectangle(24, 0, 24, 24), new Color(0, 100, 100, 50));

                batch.DrawString(font, ">", new Vector2(str_width, (int)height - str_height), Color.White);
                if (text.Length > 0)
                {
                    batch.DrawString(font, text, new Vector2(str_width * 2, (int)height - str_height), Color.White);
                }

                var cx = (float)str_width * 2;
                if (text.Length > 0)
                {
                    var txt = text.Substring(0, (int)cursor.X);
                    cx += font.MeasureString(txt).X;
                }

                for(int i = 0; i < history.Count; i++)
                {
                    batch.DrawString(font, history[history.Count - 1 - i], new Vector2(str_width * 2, (int)height - ((i + 2) * str_height)), Color.White);
                }

                batch.Draw(image, new Vector2(cx, (int)height - str_height + 2), new Rectangle(24, 0, 4, 24), Color.White);
            }
        }
    }
}
