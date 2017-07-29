using Desire_And_Doom.Utils;
using Microsoft.Xna.Framework;
using Penumbra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Desire_And_Doom.Screens
{
    class Intro_Logos_Screen : Screen
    {
        Tasker tasker;

        Texture2D SkyVaultLogo;
        Texture2D MonoGameLogo;

        Vector2 Logo_Position;
        int Logo_Size = 320;

        float SkyLogoAlpha = 0;
        float MonoLogoAlpha = 0;

        float SkyLogoY = DesireAndDoom.ScreenHeight;
        float MonoLogoY = DesireAndDoom.ScreenHeight;

        Screen_Manager screen_manager;

        public Intro_Logos_Screen(Screen_Manager _manager, Camera_2D _camera, PenumbraComponent _penumbra) : base("Logo")
        {
            _camera.Zoom = 1;
            _penumbra.AmbientColor = Color.White;

            screen_manager = _manager;

            Logo_Position = new Vector2(
                DesireAndDoom.ScreenWidth / 2,
                DesireAndDoom.ScreenHeight / 2
            );
            SkyLogoY += Logo_Size;
            MonoLogoY += Logo_Size;
        }

        public override void Load()
        {
            SkyVaultLogo = Assets.It.Load_Texture("logo", "Logo");
            MonoGameLogo = Assets.It.Load_Texture("SquareLogo_1024px", "Mono");

            float timing = 0.02f;

            tasker = new Tasker(
                (time) =>
                {
                    SkyLogoY = Math2.Lerp(SkyLogoY, DesireAndDoom.ScreenHeight / 2 - Logo_Size / 2, 0.08f);
                    if (SkyLogoY < DesireAndDoom.ScreenHeight / 2 - (Logo_Size / 2) + 1)
                        tasker.Next();
                },
                (time) =>
                {
                    SkyLogoY = Math2.Lerp(SkyLogoY, -Logo_Size * 1.2f, 0.08f);
                    if (SkyLogoY < -(Logo_Size * 1.2f) + 1)
                        tasker.Next();
                },
                (time) =>
                {
                    MonoLogoY = Math2.Lerp(MonoLogoY, DesireAndDoom.ScreenHeight / 2 - Logo_Size / 2, 0.08f);
                    if (MonoLogoY < DesireAndDoom.ScreenHeight / 2 - (Logo_Size / 2) + 1)
                        tasker.Next();
                },
                (time) =>
                {
                    MonoLogoY = Math2.Lerp(MonoLogoY, -Logo_Size * 2, 0.08f);
                    if (MonoLogoY < -(Logo_Size * 2) + 1)
                        tasker.Next();
                },
                (time) =>
                {
                    screen_manager.Goto_Screen("Menu", true);
                    tasker.Next();
                }, (time) => { }
                );   
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            if (Input.It.Is_Key_Pressed(Microsoft.Xna.Framework.Input.Keys.Enter))
                screen_manager.Goto_Screen("Menu");

            tasker.Update(time);
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(
                MonoGameLogo,
                new Rectangle(
                    (int)Logo_Position.X - Logo_Size / 2,
                    (int)MonoLogoY,
                    Logo_Size,
                    Logo_Size),
                Color.White
                );

            batch.Draw(
                SkyVaultLogo,
                new Rectangle(
                    (int)Logo_Position.X - Logo_Size / 2,
                    (int)SkyLogoY,
                    Logo_Size,
                    Logo_Size),
                Color.White
                );
        }

        public override void Destroy()
        {
            base.Destroy();

            Assets.It.Remove("Logo", typeof(Texture2D));
            Assets.It.Remove("Mono", typeof(Texture2D));
            SkyVaultLogo.Dispose();
            MonoGameLogo.Dispose();
        }
    }
}
