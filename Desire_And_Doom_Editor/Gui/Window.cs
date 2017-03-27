using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Desire_And_Doom_Editor.Gui;

namespace Desire_And_Doom_Editor.cs.Gui
{
    class Window : Panel
    {
        Button drag;
        Button close;
        Button mini;

        public float Bar_Height { get; set; } = 24;

        bool started = false;
        Vector2 start_pos = Vector2.Zero;
        public string Title { get; set; } = "";

        public Window(Monogui gui, float Width = 300, float Height = 200, string title = "",bool scale = false) : base(gui, scale)
        {
            Title = title;
            Scale = false;
            Snap_Side = Side.NONE;
            Width   = 300;
            Height  = 200;

            X = Editor.WIDTH / 2 - Width / 2;
            Y = Editor.HEIGHT / 2 - Height / 2;
            Layer = 0.8f;
            FillColor = new Color(10, 10, 10, 100);

            var font = Assets.It.Get<SpriteFont>("font");
            var drag_height = Bar_Height;

            drag = new Button(GUI, " ", font) {
                Width = Width,
                Height = drag_height,
                FillColor = new Color(10, 10, 10, 100),
                X = 0,
                Y = 0,
                Parent = this,
                Layer = 0.9f,
                Callback = () =>
                {
                    start_pos = Input.It.Mouse_Position();
                    started = true;
                    return true;
                }
            };
            Children.Add(drag);

            close = new Button(GUI, " ", font) {
                Height = drag_height,
                Width = drag_height,
                FillColor = new Color(255, 0, 0, 100),
                TextColor = Color.LightPink,
                Layer = 1,
                Hide = false,
                Callback = () => {RemoveAll(); return true; }
            };
            close.X = drag.Width - close.Width;
            drag.Add(close);

            mini = new Button(GUI, " ", font) {
                Height = drag_height,
                Width = drag_height,
                FillColor = new Color(0, 150, 255, 100),
                TextColor = Color.LightBlue,
                Layer = 1,
                Hide = false,
                Callback = () => { Toggle_Content(); return true; }
            };
            mini.X = drag.Width - close.Width - mini.Width;
            drag.Add(mini);

            var label = new Label(GUI, font, Title) {
                TextColor = new Color(255, 255, 255, 255),
                FillColor = new Color(0, 0, 0, 0),
                Width = font.MeasureString(Title).X,
                Height = Bar_Height,
                Layer = 1,
                Can_Intersect_Mouse = false
            };
            label.X += 4;
            drag.Add(label);
        }

        public void Toggle_Content()
        {
            this.Hide = !this.Hide;
            Children.ForEach(e => {
                if (e != drag && e != close && e != mini)
                    e.Hide_Toggle();
            });
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            Width = 300;
            Height = 200;

            if (started)
            {
                var mpos = Input.It.Mouse_Position();
                X -= (float)start_pos.X - mpos.X;
                Y -= (float)start_pos.Y - mpos.Y;
                start_pos = mpos;
            }

            if (Input.It.Mouse_Left() == false) started = false;

        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
    }
}
