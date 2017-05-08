using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Desire_And_Doom.ECS;
using Penumbra;
using Desire_And_Doom.Graphics;
using Desire_And_Doom.Graphics.Particle_Systems;
using NLua;

namespace Desire_And_Doom.Screens
{
    class Level_1_Screen : Game_Screen
    {
        Sky_Renderer sky;
        World world;

        public Level_1_Screen(World _world, Camera_2D _camera, PenumbraComponent _lighting, Particle_World _particle_world, Physics_Engine _physics_engine, Lua lua) : base(_world, _camera, _lighting, _particle_world, _physics_engine, lua, "Level 1")
        {
            sky = new Sky_Renderer(Assets.It.Get<Texture2D>("sky_1"));
            world = _world;
        }

        public override void Load()
        {
            lighting.AmbientColor = new Color(0.4f, 0.4f, 0.4f, 1.0f);
            Load_Map("Demo_Practice");
            
            camera.Zoom = Game1.SCALE;

            int x = -1;
            int y = -1;
        }
        public override void Update(GameTime time)
        {
            sky.Update(time);
            base.Update(time);
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            if (this.Map.Has_Sky)
                sky.Draw(batch);
        }
    }
}
