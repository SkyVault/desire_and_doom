using Desire_And_Doom.ECS;
using Desire_And_Doom.Graphics;
using Penumbra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NLua;

namespace Desire_And_Doom.Screens
{
    class Game_Screen : Screen
    {
        protected World                 world;
        protected GameCamera             camera;
        protected PenumbraComponent     lighting;
        protected Particle_World        particle_world;
        protected Physics_Engine        physics_engine;
        protected Lua lua;
        public Tiled_Map                Map { get; private set; }

        bool can_change = true;

        public Game_Screen(World _world, GameCamera _camera, PenumbraComponent _lighting, Particle_World _particle_world, Physics_Engine _physics_engine,Lua _lua, string _id) : base(_id)
        {
            world           = _world;
            camera          = _camera;
            lighting        = _lighting;
            particle_world  = _particle_world;
            physics_engine  = _physics_engine;
            lua             = _lua;
        }

        public override void Destroy()
        {
            base.Destroy();
            physics_engine.Clear_Solids();
            world.Destroy_All();
            lighting.Lights.Clear();
            Map?.Destroy();
            particle_world.Destroy();
        }

        public override void Update(GameTime time)
        {
            base.Update(time);
            Map.Update(time);
            if (!can_change) can_change = true;
        }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
            Map.Draw(batch);
        }

        public bool Load_Map(string id, float x = -1, float y = -1)
        {
            if (!can_change) return false;
            can_change = false;

            Destroy();

            Map = new Tiled_Map(id, camera, world, this, particle_world, lua, lighting) {
                Change_Scene_Callback = Load_Map
            };

            if (x != -1 && y != -1)
            {
                var list = world.Get_All_With_Component(Component.Types.Player);
                if (list.Count > 0 )
                {
                    var player = list.Last();
                    if (player != null)
                    {
                        var body = (Body)player.Get(Component.Types.Body);
                        body.X = x;
                        body.Y = y;
                    }
                }
            }

            return true;
        }

    }
}
