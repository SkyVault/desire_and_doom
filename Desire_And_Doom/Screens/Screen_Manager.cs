using Desire_And_Doom.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.Screens
{
    class Screen_Manager
    {
        Dictionary<string, Screen> screens;
        Screen active;

        Tasker fade_task;

        float fade_timer = 0;
        bool fade = false;

        float transparency = 0f;

        public Screen_Manager()
        {
            screens = new Dictionary<string, Screen>();
        }

        public void Register(Screen _screen, bool _goto = false) {
            this.screens.Add(_screen.ID, _screen);

            if (_goto)
                Goto_Screen(_screen.ID);
        }

        public void Goto_Screen(string _id, bool fade_in_out = false)
        {
            fade = fade_in_out;

            if ( fade )
            {
                var total_time = 2f;
                fade_task = new Tasker(
                    (time) => {
                        // set fader
                        fade_timer = total_time;
                        fade_task.Next();
                        return true;
                    },
                    (time) => {
                        // fade to black
                        if (fade_timer >= 0 ) {
                            transparency = (total_time - fade_timer) / total_time;
                            fade_timer -= (float) time.ElapsedGameTime.TotalSeconds;
                        }
                        else {
                            fade_task.Next();
                        }
                        return true;
                    },
                    (time) => {
                        // change screen while in black
                        active?.Destroy();
                        active = screens[_id];
                        active?.Load();

                        fade_timer = 0f;
                        fade_task.Next();
                        return true;
                    },
                    (time) => {
                        // fade to white
                        if ( fade_timer < total_time )
                        {
                            transparency = (total_time - fade_timer) / fade_timer;
                            fade_timer += (float) time.ElapsedGameTime.TotalSeconds;
                        }
                        else
                        {
                            fade_task.Next();
                        }
                        return true;
                    }
                    );
            }else
            {
                active?.Destroy();
                active = screens[_id];
                active?.Load();
            }
        }

        public void Update(GameTime time)
        {
            fade_task?.Update(time);
            active?.Update(time);
        }

        public void Draw(SpriteBatch batch)
        {
            active?.Draw(batch);

            batch.Draw(
                Assets.It.Get<Texture2D>("gui"), 
                new Rectangle(0, 0, Game1.WIDTH, Game1.HEIGHT), 
                new Rectangle(24, 0, 24, 24), 
                new Color(0f,0f,0f, transparency), 
                0f, 
                Vector2.Zero, 
                SpriteEffects.None, 
                1);
        }
    }
}
