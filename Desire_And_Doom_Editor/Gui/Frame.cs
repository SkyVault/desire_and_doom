using Desire_And_Doom_Editor.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Shapes;
using MonoGame.Extended;

namespace Desire_And_Doom_Editor.cs.Gui
{
    class Frame : Element
    {
        public int LineThickness { get; set; } = 2;

        public Frame(Monogui gui) : base(gui)
        {
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.DrawRectangle(Shape, FillColor, LineThickness);
        }
    }
}
