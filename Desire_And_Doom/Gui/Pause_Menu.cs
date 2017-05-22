using Desire_And_Doom.Screens;
using Desire_And_Doom.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.Gui
{
    class Pause_Menu
    {
        Camera_2D camera;
        
        Texture2D rect;
        Vector2 pre_origin;
        Named_Action_List actions;
        SpriteFont font;

        int selector = 0;

        public Pause_Menu(Screen_Manager screen_manager, Camera_2D _camera)
        {
            camera = _camera;

            rect = Assets.It.Get<Texture2D>("gui-rect");
            font = Assets.It.Get<SpriteFont>("gfont");

            actions = new Named_Action_List(new Dictionary<string, Action> {
                {"Resume", ()=>{
                    Game1.Toggle_Pause();
                } },
                {"Settings", ()=>{

                } },
                {"Exit",()=>{
                    Game1.Toggle_Pause();
                    screen_manager.Goto_Screen("Menu", true);
                } }
            });
        }

        public void Reset()
        {
            selector = 0;
        }

        public void Update(GameTime time)
        {

            if (Input.It.Is_Key_Pressed(Keys.Down))
                selector++;
            if (Input.It.Is_Key_Pressed(Keys.Up))
                selector--;

            if (selector >= actions.Names.Length) selector = 0;
            if (selector < 0) selector = actions.Names.Length - 1;

            if (Input.It.Is_Key_Pressed(Keys.Enter))
            {
                actions.Call(selector);
            }
        }

        public void Draw(SpriteBatch batch)
        {
            var scale = 1f;

            // draw options
            var names = actions.Names;
            int index = 0;
            foreach (var name in names)
            {
                var size = font.MeasureString(name) * scale;
                float margin = 16;
                float x = Game1.WIDTH / 2 - size.X / 2;
                float y = Game1.HEIGHT / 2 - (size.Y + margin) / 2 - ((size.Y + margin) * names.Length / 2) + (index * (size.Y + margin)) + size.Y / 2;

                batch.DrawString(
                    font,
                    name,
                    new Vector2(x, y),
                    (selector == index) ? Color.Black : Color.White,
                    0f,
                    Vector2.Zero,
                    scale,
                    SpriteEffects.None,
                    1f
                    );

                if (selector == index)
                {
                    batch.Draw(
                    rect,
                    new Rectangle(
                        (int)((Game1.WIDTH / 2) - Game1.WIDTH / 8),
                        (int)(y + size.Y * 0.1f),
                        (int)(Game1.WIDTH / 4),
                        (int)(size.Y * 0.9f)),
                    new Rectangle(0, 0, 512, 512),
                    Color.Orange,
                    0f,
                    Vector2.Zero,
                    SpriteEffects.None,
                    0.8f
                    );
                }

                index++;
            }

            // draw the rectangle
            batch.Draw(
                rect,
                new Rectangle(
                    (int)(Game1.WIDTH / 2 - (128 * 1.5f)),
                    Game1.HEIGHT / 2 - Game1.HEIGHT / 3,
                    128 * 3,
                    (int)(Game1.HEIGHT / 1.5f)
                    ),
                new Rectangle(0, 0, 512, 512),
                new Color(0, 0, 0, 0.8f),
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                0.5f
                );
        }

    }
}
