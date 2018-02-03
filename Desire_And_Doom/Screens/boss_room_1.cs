using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desire_And_Doom.ECS;
using Desire_And_Doom.Graphics;
using Penumbra;
using Microsoft.Xna.Framework.Content;
using Desire_And_Doom.Entities;
using Microsoft.Xna.Framework;
using NLua;
using Microsoft.Xna.Framework.Graphics;

namespace Desire_And_Doom.Screens
{
    class Boss_Room_1 : Game_Screen
    {
        ContentManager content;

        public Boss_Room_1(
            World               _world, 
            GameCamera           _camera, 
            PenumbraComponent   _lighting, 
            Particle_World      _particle_world, 
            Physics_Engine      _physics_engine, 
            ContentManager      _content,
            Lua                 _lua,
            GraphicsDevice      _device) 
            : base(_world, _camera, _lighting, _particle_world, _physics_engine, _lua, _device, "Boss Room 1")
        {
            this.content    = _content;
        }

        public override void Load()
        {
            base.Load();

            Load_Map("DemoArena");

            //Boss_1.Create(lua, world, particle_world,new Vector2(300, 300));

        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }
}
