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

namespace Desire_And_Doom.Screens
{
    class Level_1_Screen : Game_Screen
    {
        Sky_Renderer sky;
        //World world;

        Pause_Menu pause_menu;

        public Level_1_Screen(Screen_Manager screen_manager, World _world, Camera_2D _camera, PenumbraComponent _lighting, Particle_World _particle_world, Physics_Engine _physics_engine, Lua lua) : base(_world, _camera, _lighting, _particle_world, _physics_engine, lua, "Level 1")
        {
            sky = new Sky_Renderer(Assets.It.Get<Texture2D>("sky_1"), false);
            world = _world;
            pause_menu = new Pause_Menu(screen_manager, camera);
        }

        public override void Load()
        {
            lighting.AmbientColor = new Color(0.4f, 0.4f, 0.4f, 1.0f);
            Load_Map("Dungeon_Room_2");
            
            camera.Zoom = DesireAndDoom.SCALE;
            if (DesireAndDoom.Game_State == DesireAndDoom.State.PAUSED)
                DesireAndDoom.Toggle_Pause();
        }

        //override 

        public override void Update(GameTime time)
        {
            sky.Update(time);
            base.Update(time);

            pause_menu.Update(time);

            if (Input.It.Is_Key_Pressed(Keys.Escape))
            {
                pause_menu.Reset();
                DesireAndDoom.Toggle_Pause();
            }

            
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            if (this.Map.Has_Sky)
                sky.Draw(batch);
        }

        public override void UIDraw(SpriteBatch batch)
        {
            base.UIDraw(batch);

            if (DesireAndDoom.Game_State == DesireAndDoom.State.PAUSED)
                pause_menu.Draw(batch);
        }
    }
}
