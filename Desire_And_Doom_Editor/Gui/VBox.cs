using Desire_And_Doom_Editor.Gui;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Desire_And_Doom_Editor.cs.Gui
{
    class VBox : Panel
    {
        public float Margin { get; set; } = 10f;
        public VBox(Monogui gui) : base(gui)
        {
        }

        public override void Update(GameTime time)
        {
            base.Update(time);
            int i = 0;
            foreach (var e in Children)
            {
                if (i > 0) e.Y = Children[i - 1].Y + Children[i - 1].Height;
                i++;
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
    }
}
