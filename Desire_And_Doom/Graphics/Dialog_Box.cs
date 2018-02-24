using Desire_And_Doom.Graphics;
using Desire_And_Doom.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public int GetNextDialogPointer() {
            if (IsOpen == false) return 0;
            return
                CurrentDialog.Dialog_Texts[CurrentDialogTextPointer].NextDialogText;
        }

        PrimitivesBatch primitives;
        public Dialog_Box(PrimitivesBatch _primitives) {
            primitives = _primitives;
        }

        public bool TryOpen(Dialog dialog) {
            if (!IsOpen) {
                CurrentDialogTextPointer = 1;
                CurrentDialog = dialog;
                return true;
            }
            return false;
        }

        public void Update(GameTime time) {
            if (!IsOpen) return;

            if (Input.It.Is_Key_Pressed(Keys.Enter)) {
                if (CurrentDialog.Dialog_Texts[CurrentDialogTextPointer].options.Count() == 0) {
                    CurrentDialogTextPointer = GetNextDialogPointer();
                    if (CurrentDialogTextPointer == 0)
                    {
                        CurrentDialog = null;
                    }
                }
            }
        }

        public void Draw(SpriteBatch batch) {
            if (!IsOpen) return;

            var height = DesireAndDoom.ScreenHeight / 3;
            var color = new Color(0.2f, 0.2f, 0.2f, 0.9f);

            var font = Assets.It.Get<SpriteFont>("gfont");

            var text = CurrentDialog.Dialog_Texts[CurrentDialogTextPointer].Value;

            primitives.DrawFilledRect(
                new Vector2(0, DesireAndDoom.ScreenHeight - height),
                new Vector2(DesireAndDoom.ScreenWidth, height),
                color,
                0,
                0.99f
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
        }
    }
}
