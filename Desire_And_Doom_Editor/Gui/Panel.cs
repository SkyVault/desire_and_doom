using Desire_And_Doom_Editor.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Desire_And_Doom_Editor.cs.Gui
{
    class Panel : Element
    {
        public enum Side { LEFT, RIGHT, NONE };
        public Side Snap_Side = Side.NONE;

        public bool Scale { get; set; } = true;
        public bool Scale_Children { get; set; } = true;

        bool started = false;
        Vector2 start_pos = Vector2.Zero;
        Button scale_btn;

        public Panel(Monogui gui, bool scale = false) : base(gui)
        {
            Scale = scale;
            if (scale)
            {
                scale_btn = new Button(GUI, ">", Assets.It.Get<SpriteFont>("font"))
                {
                    X = X,
                    Y = Y,
                    Width = 16,
                    Height = Editor.HEIGHT,
                    FillColor = new Color(0, 0, 0, 20),
                    Callback = () => {Scale_Clicked_Start(); return true; }
                };
                Children.Add(scale_btn);
            }
        }

        public void Scale_Clicked_Start()
        {
            start_pos = Input.It.Mouse_Position();
            started = true;
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            if (Scale)
            {
                if (started)
                {
                    var mpos = Input.It.Mouse_Position();
                    Width += (float)start_pos.X - mpos.X;
                    start_pos = mpos;
                }

                if (Input.It.Mouse_Left() == false) started = false;

                scale_btn.X = X;
                scale_btn.Y = Y;
                scale_btn.Width = 16;
                scale_btn.Height = Height;
            }

            if (Snap_Side == Side.LEFT) X = 0;
            if (Snap_Side == Side.RIGHT) X = Editor.WIDTH - Width;
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
    }
}
