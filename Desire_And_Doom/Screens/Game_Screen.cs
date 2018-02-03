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
using Desire_And_Doom.ECS.Systems;

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
        private GraphicsDevice device;

        bool can_change = true;

        public Game_Screen(World _world, GameCamera _camera, PenumbraComponent _lighting, Particle_World _particle_world, Physics_Engine _physics_engine,Lua _lua, GraphicsDevice _device, string _id) : base(_id)
        {
            world           = _world;
            camera          = _camera;
            lighting        = _lighting;
            particle_world  = _particle_world;
            physics_engine  = _physics_engine;
            lua             = _lua;
            device          = _device;
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

        public void DestroyAllButPersistant()
        {
            base.Destroy();
            physics_engine.Clear_Solids();

            var persistant = world.Get_All_Persistant();
            world.Destroy_All(true);

            lighting.Lights.Clear();

            //world.

            foreach (var ent in persistant)
            {
                if (ent.Has(Component.Types.Light_Emitter))
                {
                    var light = (Light_Emitter)ent.Get(Component.Types.Light_Emitter);
                    lighting.Lights.Add(light.Light);
                }
            }

            Map?.Destroy();
            particle_world.Destroy();
        }

        public override void Update(GameTime time)
        {
            base.Update(time);
            Map.Update(time);
            if (!can_change) can_change = true;
        }

        public override void PreDraw(SpriteBatch batch)
        {
            base.PreDraw(batch);
            Map.PreDraw(batch);
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

            DestroyAllButPersistant();

            Map = new Tiled_Map(id, camera, world, this, particle_world, lua, device, lighting, true) {
                Change_Scene_Callback = Load_Map
            };

            // TODO(Dustin): Need to find a less shitty way of doing this...
            ((Animation_Renderer_System)world.Get_System<Animation_Renderer_System>())?.Give_Tile_Map(Map);
            ((Sprite_Renderer_System)world.Get_System<Sprite_Renderer_System>())?.Give_Tile_Map(Map);
            ((Advanced_Animation_Rendering_System)world.Get_System<Advanced_Animation_Rendering_System>())?.Give_Tile_Map(Map);
            ((Multipart_Animation_System)world.Get_System<Multipart_Animation_System>())?.Give_Tile_Map(Map);

            if (x != -1 && y != -1)
            {
                var list = world.Get_All_With_Component(Component.Types.Player);
                if (list.Count > 0 )
                {
                    var player = list.Last();
                    if (player != null)
                    {
                        var body = (Body)player.Get(Component.Types.Body);
                        var physics = (Physics)player.Get(Component.Types.Physics);

                        body.X = x;
                        body.Y = y;

                        physics.Velocity = Vector2.Zero;
                    }
                }
            }

            return true;
        }

    }
}
