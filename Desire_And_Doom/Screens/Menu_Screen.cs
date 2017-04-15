using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Nez.UI;
using Microsoft.Xna.Framework;
using Desire_And_Doom.Utils;
using Penumbra;

namespace Desire_And_Doom.Screens
{
    class Menu_Screen : Screen
    {
        Screen_Manager manager;
        Tasker tasker;

        Vector2 logo_position;
        PenumbraComponent penumbra;
        Camera_2D camera;

        Vector2 target;

        float time_scale = 50;

        public Menu_Screen(Screen_Manager _manager, PenumbraComponent _penumbra, Camera_2D _camera) : base("Menu")
        {
            camera = _camera;
            manager = _manager;
            penumbra = _penumbra;
        }

        public override void Load()
        {
            camera.Zoom = 1;
            penumbra.AmbientColor = new Color(1f, 1f, 1f, 1f);
            logo_position = new Vector2(Game1.WIDTH / 2 - 128, Game1.HEIGHT / 2 - 128);
            target = logo_position;
            tasker = new Tasker(
                (time) => {
                    Timers.It.New_Timer(() => {
                        tasker.Next();
                        return true; }, 1);
                    tasker.Next();
                    return true;
                },
                (time) => {return true;},
                (time) => {
                    time_scale = 20;
                    target.Y = Game1.HEIGHT / 2 + 32;
                    if ( Vector2.Distance(logo_position, target) < 20 ) {
                        tasker.Next();
                        time_scale = 20;
                    }
                    return true;
                },
                (time) => {
                    time_scale -= (float) time.ElapsedGameTime.TotalSeconds * 10;
                    if ( time_scale <= 0 ) time_scale = 1;
                    target.Y = -500;
                    if (Vector2.Distance(logo_position, target) < 100)
                        tasker.Next();
                    return true;
                },
                (time) => {
                    manager.Goto_Screen("Level 1", true);
                    tasker.Next();
                    return true;
                }
                );
        }

        public override void Update(GameTime time)
        {

            float dx = target.X - logo_position.X;
            float dy = target.Y - logo_position.Y;

            logo_position.X += dx / time_scale;
            logo_position.Y += dy / time_scale;

            base.Update(time);
            tasker?.Update(time);
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(
                Assets.It.Get<Texture2D>("Logo"), 
                new Rectangle((int)logo_position.X, (int)logo_position.Y, 256, 256),
                new Rectangle(0, 0, 591, 573),
                Color.White,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                1   
                );
        }

        public override void Destroy()
        {

            base.Destroy();
        }
    }
}
