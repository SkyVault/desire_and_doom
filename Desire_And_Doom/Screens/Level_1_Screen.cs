using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Desire_And_Doom.ECS;
using Penumbra;
using Desire_And_Doom.Graphics;
using Desire_And_Doom.Graphics.Particle_Systems;
using NLua;
using System;
using Microsoft.Xna.Framework.Input;
using Desire_And_Doom.Gui;
using Microsoft.Xna.Framework.Media;

namespace Desire_And_Doom.Screens
{
    class Level_1_Screen : Game_Screen
    {
        Sky_Renderer sky;
        
        // NOTE(Dustin): Maybe we need to extract this out somewhere else?
        Pause_Menu pause_menu;

        public Level_1_Screen(Screen_Manager screen_manager, World _world, GameCamera _camera, PenumbraComponent _lighting, Particle_World _particle_world, Physics_Engine _physics_engine, Lua lua, GraphicsDevice device) : base(_world, _camera, _lighting, _particle_world, _physics_engine, lua, device,"Level 1")
        {
            sky = new Sky_Renderer(Assets.It.Get<Texture2D>("sky_1"), false);
            world = _world;
            pause_menu = new Pause_Menu(screen_manager, camera);
        }

        public override void Load(params string []args)
        {
            world.Destroy_All();
            lighting.AmbientColor = new Color(0.6f, 0.6f, 0.6f, 1.0f);
            if (args.Length > 0)
            {
                Load_Map(args[0]);
            }
            //Load_Map("Ship");
            //Load_Map("Demo");
            //Load_Map("Dungeon_Floor_1");

            var ai_system = (AI_System)world.Get_System<AI_System>();
            ai_system.Give_Map(Map);

            //var song = Song.FromUri("Bloom", new Uri("Content/Audio/Bloom.mp3", UriKind.Relative)); 
            //MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;

            camera.Zoom = DesireAndDoom.SCALE;
            DesireAndDoom.Request_Resume(); // Make sure the game is unpaused
        }

        public override void Destroy()
        {
            MediaPlayer.Stop();
            base.Destroy();
        }

        public override void Update(GameTime time)
        {
            sky.Update(time);
            base.Update(time);

            pause_menu.Update(time);
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            if (this.Map.Has_Sky)
                sky.Draw(batch);

            batch.Draw(
                Assets.It.Get<Texture2D>("Background"),
                new Rectangle(0, 0, DesireAndDoom.ScreenWidth, DesireAndDoom.ScreenHeight),
                Color.White
                );
        }

        public override void UIDraw(SpriteBatch batch)
        {
            base.UIDraw(batch);

            pause_menu.Draw(batch);
        }
    }
}
