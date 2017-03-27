using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Desire_And_Doom.ECS;
using NLua;
using Penumbra;
using Desire_And_Doom.Graphics;
using Desire_And_Doom.Graphics.Particle_Systems;

namespace Desire_And_Doom.Screens
{
    class Level_1_Screen : Screen
    {
        World world;
        Camera_2D camera;
        Tiled_Map map;
        PenumbraComponent lighting;
        Sky_Renderer sky;

        public Level_1_Screen(World _world, Camera_2D _camera, PenumbraComponent _lighting, Particle_World _particle_world) : base("Level 1")
        {
            world       = _world;
            camera      = _camera;
            lighting    = _lighting;

            sky = new Sky_Renderer(Assets.It.Get<Texture2D>("sky_1"));

            _particle_world.Add(new Fire_Fly_Emitter(new Vector2(0, 0)));
        }

        public override void Load()
        {
            //lighting.AmbientColor = new Color(20, 20, 20);

            map = new Tiled_Map("Demo", camera, world, lighting);

            //world.Create_Entity(Assets.It.Get<LuaTable>("Player"), 32, 32);
            
            //var npc = world.Create_Entity();
            //npc.Add(new Body(new Vector2(200, 200), new Vector2(8, 4)));
            //npc.Add(new Sprite(Assets.It.Get<Texture2D>("entities"), new Rectangle(0, 135, 18, 24)));
            //npc.Add(new Physics(Vector2.Zero));
            //npc.Add(new Npc(Assets.It.Get<LuaTable>("dialog_npc_test")));

            var r = new Random();
            //world.Create_Entity(Assets.It.Get<LuaTable>("Zombie"), 100, 100);
            //world.Create_Entity(Assets.It.Get<LuaTable>("Zombie"), 200, 200);
            //world.Create_Entity(Assets.It.Get<LuaTable>("Wolf"), 300, 300);

            //world.Create_Entity(Assets.It.Get<LuaTable>("Bird"), 408, 400-8);
            //world.Create_Entity(Assets.It.Get<LuaTable>("Bird"), 400+8, 400+16);
            //world.Create_Entity(Assets.It.Get<LuaTable>("Bird"), 400-8, 400-8);
            //world.Create_Entity(Assets.It.Get<LuaTable>("Bird"), 400-16, 400+8);
            //world.Create_Entity(Assets.It.Get<LuaTable>("Bird"), 400, 400);

            world.Create_Entity(Assets.It.Get<LuaTable>("Orange"), 300, 300);            
        }

        public override void Update(GameTime time)
        {
            base.Update(time);
            sky.Update(time);
        }

        public override void Draw(SpriteBatch batch)
        {
            map.Draw(batch);
            //sky.Draw(batch);
        }

        public override void Destroy()
        {
            base.Destroy();

            lighting.Lights.Clear();
        }
    }
}
