using Desire_And_Doom_Editor.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Desire_And_Doom_Editor.cs.Gui
{
    class HBox : Panel
    {
        public float Margin { get; set; } = 10f;
        public HBox(Monogui gui) : base(gui)
        {
        }

        public override void Update(GameTime time)
        {
            base.Update(time);
            int i = 0;
            foreach (var e in Children)
            {
                if (i == 0) e.X = Left;
                else e.X = (Margin * i) + Left + Children[i - 1].Width * i;
                i++;
            }
        }
    }
}
