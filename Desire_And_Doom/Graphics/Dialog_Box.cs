using Desire_And_Doom.Graphics;
using Desire_And_Doom.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public bool IsOpen { get => CurrentDialog != null; }

        PrimitivesBatch primitives;
        public Dialog_Box(PrimitivesBatch _primitives) {
            primitives = _primitives;
        }

        public bool TryOpen(Dialog dialog) {
            if (!IsOpen) {
                CurrentDialog = dialog;
                return true;
            }
            return false;
        }

        public void Update(GameTime time) {
            if (!IsOpen) return;

        }

        public void Draw(SpriteBatch batch) {
            if (!IsOpen) return;

            primitives.DrawFilledRect(
                new Vector2(0, 0),
                new Vector2(DesireAndDoom.ScreenWidth, DesireAndDoom.ScreenHeight / 3),
                Color.Blue
            );
        }
    }
}
