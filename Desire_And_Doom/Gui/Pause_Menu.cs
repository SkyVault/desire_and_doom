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
        GameCamera camera;
        
        Texture2D rect;
        Named_Action_List actions;
        SpriteFont font;

        int selector = 0;

        bool showing = false;

        public Pause_Menu(Screen_Manager screen_manager, GameCamera _camera)
        {
            camera = _camera;

            rect = Assets.It.Get<Texture2D>("gui-rect");
            font = Assets.It.Get<SpriteFont>("gfont");

            actions = new Named_Action_List(new Dictionary<string, Action> {
                {"Resume", ()=>{
                    DesireAndDoom.Request_Resume();
                    showing = false;
                } },
                {"Settings", ()=>{

                } },
                {"Level Select", () =>
                {
                    showing = false;
                    screen_manager.Goto_Screen("Level Select", false);
                }},
                {"Exit",()=>{
                    //DesireAndDoom.Request_Resume();
                    showing = false;
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

            if (Input.It.Is_Key_Pressed(Keys.Escape) || Input.It.Is_Gamepad_Button_Pressed(Buttons.Start)) 
            {
                Reset(); 
                DesireAndDoom.Toggle_Pause();
                showing = DesireAndDoom.Game_State == DesireAndDoom.State.PAUSED;
            } 
            
            if (Input.It.Is_Gamepad_Button_Pressed(Buttons.B) && showing)
            {
                Reset();
                DesireAndDoom.Request_Resume();
                showing = DesireAndDoom.Game_State == DesireAndDoom.State.PAUSED;
            }

            if (!showing) return;

            var down = Input.It.Is_Key_Pressed(Keys.Down) || Input.It.Is_Gamepad_Button_Pressed(Buttons.DPadDown) || Input.It.Is_Gamepad_Button_Pressed(Buttons.LeftThumbstickDown);
            var up = Input.It.Is_Key_Pressed(Keys.Up) || Input.It.Is_Gamepad_Button_Pressed(Buttons.DPadUp) || Input.It.Is_Gamepad_Button_Pressed(Buttons.LeftThumbstickUp);

            if (down) selector++;
            if (up) selector--;

            if (selector >= actions.Names.Length) selector = 0;
            if (selector < 0) selector = actions.Names.Length - 1;

            if (Input.It.Is_Key_Pressed(Keys.Enter) || Input.It.Is_Gamepad_Button_Pressed(Buttons.A))
            {
                actions.Call(selector);
            }
        }

        public void Draw(SpriteBatch batch)
        {

            if (!showing) return;

            var scale = 1f;

            // draw options
            var names = actions.Names;
            int index = 0;
            var box_width = DesireAndDoom.ScreenWidth / 3;
            
            foreach (var name in names)
            {
                var size = font.MeasureString(name) * scale;
                float margin = 16;
                float x = DesireAndDoom.ScreenWidth / 2 - (size.X / 2);
                float y = DesireAndDoom.ScreenHeight / 2 - (size.Y + margin) / 2 - ((size.Y + margin) * names.Length / 2) + (index * (size.Y + margin)) + size.Y / 2;
                
                batch.DrawString(
                    font,
                    name,
                    new Vector2(x, y),
                    (selector == index) ? Color.Black : Color.White,
                    0f,
                    Vector2.Zero,
                    1,
                    SpriteEffects.None,
                    1f
                    );

                if (selector == index)
                {
                    batch.Draw(
                    rect,
                    new Rectangle(
                        (int)(DesireAndDoom.ScreenWidth / 2 - (box_width / 2)),
                        (int)(y + size.Y * 0.1f),
                        box_width,
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
                    (int)(DesireAndDoom.ScreenWidth / 2 - (box_width / 2)),
                    DesireAndDoom.ScreenHeight / 2 - DesireAndDoom.ScreenHeight / 3,
                    box_width,
                    (int)(DesireAndDoom.ScreenHeight / 1.5f)
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
