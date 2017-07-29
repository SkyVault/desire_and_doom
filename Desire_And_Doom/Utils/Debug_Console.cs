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

        public void Exit() { DesireAndDoom.SHOULD_QUIT = true; }
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
                var image = Assets.It.Get<Texture2D>("gui-rect");

                var height      = DesireAndDoom.HEIGHT * (state == State.HALF ? 0.7f : 0.15f);
                var font        = Assets.It.Get<SpriteFont>("font");
                var str_height  = (int)font.MeasureString(" ").Y;
                var str_width   = (int)font.MeasureString(">").X;

                batch.Draw(
                    image, 
                    new Rectangle(0, 0, DesireAndDoom.WIDTH, (int)height), 
                    new Rectangle(0, 0, 512, 512), 
                    new Color(10, 10, 10, 150));
                

            }
        }
    }
}
