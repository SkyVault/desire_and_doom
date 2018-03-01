using Desire_And_Doom.Graphics;
using Desire_And_Doom.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using CColor = System.Drawing.Color;

namespace Desire_And_Doom
{
    class Dialog_Box
    {
        public Dialog CurrentDialog {get; private set;} = null;
        public int CurrentDialogTextPointer {get; private set;} = 1;
        public bool IsOpen { get => CurrentDialog != null; }
        public int Selector { get; private set; } = 0;

        private float timer = 0;
        private Lua lua;

        public int GetNextDialogPointer() {
            if (IsOpen == false) return 0;
            return
                CurrentDialog.Dialog_Texts[CurrentDialogTextPointer].NextDialogText;
        }

        PrimitivesBatch primitives;
        public Dialog_Box(PrimitivesBatch _primitives, Lua _lua) {
            primitives  = _primitives;
            lua         = _lua;
        }

        public bool TryOpen(Dialog dialog) {
            if (!IsOpen && this.timer <= 0) {
                CurrentDialogTextPointer = 1;
                CurrentDialog = dialog;
                return true;
            }
            return false;
        }

        public void Update(GameTime time) {
            timer -= (float)time.ElapsedGameTime.TotalSeconds;
            if (!IsOpen) { Selector = 0; return; }

            DesireAndDoom.Request_Pause();

            if (Input.It.Is_Key_Pressed(Keys.Enter) || Input.It.Is_Key_Pressed(Keys.Z)) {
                if (CurrentDialog.Dialog_Texts[CurrentDialogTextPointer].options.Count() == 0) {
                    CurrentDialogTextPointer = GetNextDialogPointer();
                    if (CurrentDialogTextPointer == 0)
                    {
                        timer = Constants.DIALOG_COOLDOWN;
                        CurrentDialog = null;
                        DesireAndDoom.Request_Resume();
                        return;
                    }
                } 
            }

            var dialog_text = CurrentDialog.Dialog_Texts[CurrentDialogTextPointer];
            if (dialog_text.options.Count > 0)
            {
                if (Input.It.Is_Key_Pressed(Keys.Left) || Input.It.Is_Key_Pressed(Keys.Up)) 
                {
                    Selector--;
                } 
                else if (Input.It.Is_Key_Pressed(Keys.Right) || Input.It.Is_Key_Pressed(Keys.Down))
                {
                    Selector++;
                }

                if (Input.It.Is_Key_Pressed(Keys.Enter) || Input.It.Is_Key_Pressed(Keys.Z))
                {
                    var the_option = dialog_text.options[Selector];
                    CurrentDialogTextPointer = the_option.NextDialogText;
                    if (the_option.action != null)
                    {
                        the_option.action.Call();
                    }
                }

                Selector = Selector < 0 ? dialog_text.options.Count - 1 : (Selector > dialog_text.options.Count - 1 ? 0 : Selector);
            }
        }

        public void Draw(SpriteBatch batch) {
            if (!IsOpen) return;
            if (CurrentDialogTextPointer == 0)
            {
                CurrentDialog = null;
                DesireAndDoom.Request_Resume();
                return;
            }

            var height = DesireAndDoom.ScreenHeight / 3;
            var color = new Color(0.2f, 0.2f, 0.2f, 0.9f);

            var font = Assets.It.Get<SpriteFont>("gfont");

            var dialog_text = CurrentDialog.Dialog_Texts[CurrentDialogTextPointer];
            var text = dialog_text.Value;

            primitives.DrawFilledRect(
                new Vector2(0, DesireAndDoom.ScreenHeight - height),
                new Vector2(DesireAndDoom.ScreenWidth, height),
                color,
                0,
                0.98f
            );

            batch.DrawString(
                    font,
                    text,
                    new Vector2(32, DesireAndDoom.ScreenHeight - height + 32),
                    Color.White,
                    0.0f,
                    Vector2.Zero,
                    0.5f,
                    SpriteEffects.None,
                    1.0f
                    );

            if (dialog_text.options.Count > 0)
            {
                var x = 0.0f;
                var index = 0;
                foreach(var option in dialog_text.options)
                {
                    var margin = 16;
                    var tsize = font.MeasureString(option.Value) * 0.5f;
                    var size_x = tsize.X + margin;
                    batch.DrawString(
                        font,
                        option.Value,
                        new Vector2(32 + x, DesireAndDoom.ScreenHeight - 32 - margin),
                        Color.White,
                        0.0f,
                        Vector2.Zero,
                        0.5f,
                        SpriteEffects.None,
                        1.0f
                    );

                    if (index == Selector)
                    {
                        var rect = Assets.It.Get<Texture2D>("gui-rect");
                        batch.Draw(
                        rect,
                        new Rectangle((int)(32 + x), DesireAndDoom.ScreenHeight - 32 - margin, (int)tsize.X, (int)tsize.Y),
                        new Rectangle(0, 0, 512, 512),
                        Color.Orange,
                        0f,
                        Vector2.Zero,
                        SpriteEffects.None,
                        0.99f
                        );
                    }

                    x += size_x;
                    index++;
                }
            }
        }
    }
}
