using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Desire_And_Doom_Editor.cs.Gui
{
    class NumericInputBox : Panel
    {
        public string Text { get; set; } = "";

        public NumericInputBox(Monogui gui, bool scale = false) : base(gui, scale)
        {
            Width = 64;
            Height = 24;
        }

        public double Get_Value()
        {
            var a = double.TryParse(Text, out double result);
            return result;
        }

        public override void Key_Pressed(Keys key)
        {
            base.Key_Pressed(key);

            if (key == Keys.Back && Text.Length > 0 && Active)
                Text = Text.Substring(0, Text.Length - 1);
        }

        public override void Key_Typed(char charactor)
        {
            base.Key_Typed(charactor);
            if (!Active) return;

            var font = Assets.It.Get<SpriteFont>("font");
            if ("1234567890".Contains(charactor) && font.MeasureString(Text).X < Width - 16)
                Text += charactor;
        }

        public override void Update(GameTime time)
        {

            if (Input.It.Mouse_Left() && Mouse_In_Bounds() == false)
                Active = false;
            if (Input.It.Mouse_Left_Pressed() && Mouse_In_Bounds())
                Active = true;

            FillColor = Active ? Color.White : Color.LightGray;
            
            if (!Active) return;
            

            base.Update(time);
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            var font = Assets.It.Get<SpriteFont>("font");
            if (Text.Length > 0)batch.DrawString(font, Text, Local_Position + new Vector2(0,font.MeasureString(Text).Y / 4), Color.Black);
        }
    }
}
