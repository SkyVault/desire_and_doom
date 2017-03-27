using Desire_And_Doom_Editor.Gui;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Desire_And_Doom_Editor.cs.Gui
{
    class DropDownItem
    {
        public Func<bool> Callback;
        public string Title;
    }

    class DropDownList : Button
    {
        private VBox dropdown;
        private float Margin = 0;

        public DropDownList(Monogui gui, string title, SpriteFont font) : base(gui, title, font)
        {
            dropdown = new VBox(GUI) {
                Margin = 0,
                Width = 48,
                FillColor = new Color (10, 10, 10, 100)
            };
            Margin = 16;

            dropdown.Hide_Toggle();
        }
        
        public static DropDownList Create(Monogui GUI, SpriteFont font, string title, List<DropDownItem> callbacks)
        {
            var dropdown = new DropDownList(GUI, title, font)
            {
                Width = 48,
                Height = 32 - 4,
                Y = 2,
                Hide = false,
                FillColor = new Color(100, 100, 100, 100)
            };

            callbacks.ForEach(c => {
                dropdown.AddSub(new Button(GUI, c.Title, font)
                {
                    X = 0,
                    Y = 0,
                    Width = font.MeasureString(c.Title).X,
                    Height = 24,
                    Callback = c.Callback,
                    FillColor = new Color(0, 0, 0, 0)
                });
            });

            return dropdown;
        }

        public Element AddSub(Element child)
        {
            var btn = (Button)child;
            child.Hide = dropdown.Hide;
            return dropdown.Add(child);
        }

        public override void Update(GameTime time)
        {
            dropdown.X = Local_Left;
            dropdown.Y = Local_Bottom + 2;

            if (Input.It.Mouse_Left() && Mouse_In_Bounds() == false)
                dropdown.HideAll();

            if (Clicked)
            {
                dropdown.Hide_Toggle();
            }

            dropdown.Height = 0;
            dropdown.Children.ForEach(e => {
                if (e.Width > dropdown.Width)
                    dropdown.Width = e.Width + Margin;
                e.X = Margin / 2;

                dropdown.Height += e.Height;

                var btn = (Button)e;
                if (btn.Clicked) dropdown.Hide_Toggle();
            });
            

            base.Update(time);
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
    }
}
