using Desire_And_Doom_Editor.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Desire_And_Doom_Editor.cs.Gui;

namespace Desire_And_Doom_Editor.Gui
{
    class Button : Label
    {
        public bool Clicked { get => Input.It.Mouse_Left_Pressed() && Mouse_In_Bounds() && GUI.Mouse_On_Layer_Sorted(this);  }
        public Func<bool> Callback { get; set; }
        
        public Button(Monogui gui,string str = "", SpriteFont font = null) : base(gui, font, str)
        {
            Width = font.MeasureString(str).X;
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            if (Clicked && Active)
                Callback?.Invoke();
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
    }
}
