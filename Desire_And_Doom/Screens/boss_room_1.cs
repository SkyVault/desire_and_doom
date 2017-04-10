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

namespace Desire_And_Doom.Screens
{
    class Boss_Room_1 : Game_Screen
    {
        ContentManager content;
        Lua lua;
        public Boss_Room_1(
            World               _world, 
            Camera_2D           _camera, 
            PenumbraComponent   _lighting, 
            Particle_World      _particle_world, 
            Physics_Engine      _physics_engine, 
            ContentManager      _content,
            Lua                 _lua) 
            : base(_world, _camera, _lighting, _particle_world, _physics_engine, "Boss Room 1")
        {
            this.content    = _content;
            this.lua        = _lua;
        }

        public override void Load()
        {
            base.Load();

            Load_Map("DemoArena");

            Boss_1.Create(content, lua, world, new Vector2(300, 300));
        }
    }
}
