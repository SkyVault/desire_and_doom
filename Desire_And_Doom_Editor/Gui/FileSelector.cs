using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Shapes;
using MonoGame.Extended;
using Microsoft.Xna.Framework;

namespace Desire_And_Doom_Editor.cs.Gui
{
    class FileSelector : HBox
    {
        public string Path { get; set; } = "";
        
        public FileSelector(Monogui gui, string title, string file_types_pattern) : base(gui)
        {
            Width = 256;
            Height = 24;
            FillColor = Color.White;

            //input.Parent = this;
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.DrawRectangle(Local_Position, Size, FillColor, 2);
        }
    }
}
