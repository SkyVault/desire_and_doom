using Desire_And_Doom_Editor.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Desire_And_Doom_Editor.cs.Gui
{
    class Label : Element
    {
        public string Text { get; set; } = "";

        SpriteFont font;
        public Label(Monogui gui, SpriteFont font, string str = "") : base(gui)
        {
            this.Text = str;
            this.font = font;
        }

        public Color TextColor { get; set; } = Color.White;

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            var size = font.MeasureString(Text);
            batch.DrawString(font, Text, Center - size / 2, TextColor);
        }
    }
}
