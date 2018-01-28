using Desire_And_Doom.ECS;
using NLua;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Desire_And_Doom.Graphics;
using Desire_And_Doom.ECS.Components;
using Desire_And_Doom.Graphics.Particle_Systems;

namespace Desire_And_Doom.Entities
{
    class Boss_1
    {
        public static readonly int BOSS_HEALTH = 15;

        public static Entity Create(Lua lua, World world, Particle_World particle_world, Vector2 position)
        {
            var entity = world.Create_Entity();

            Texture2D texture = Assets.It.Get<Texture2D>("Boss_Texture");
            if (texture == null )
            {
                Console.WriteLine("ERROR::BOSS::1 requires a texture with the id Boss_Texture!");
            }

            entity.Add(new Body(position, new Vector2(24, 24)));
            entity.Add(new Physics(Vector2.Zero, Physics.PType.DYNAMIC));

            // main container for the head
            var graphics = new Multipart_Animation();

            // the base sprite
            var base_head = new Animated_Sprite(texture, "head");
            base_head.Animations.Add("head", new Animation(
                new List<Animation_Frame> {
                    new Animation_Frame(new Vector2(0, 0), new Vector2(47, 52))
                }
                , "head"));
            base_head.Offset = new Vector2(0, -17);
            base_head.Layer_Offset = 0.2f;

            var eyes = new Animated_Sprite(texture, "eye-close");
            eyes.Animations.Add("eye-close", new Animation(
                new List<Animation_Frame> {
                    new Animation_Frame(new Vector2(95, 0), new Vector2(33, 5)),
                    new Animation_Frame(new Vector2(95, 5), new Vector2(33, 5)),
                    new Animation_Frame(new Vector2(95, 10), new Vector2(33, 5)),
                    new Animation_Frame(new Vector2(95, 15), new Vector2(33, 5)),
                },
                "eye-close"));
            eyes.Layer_Offset = 0.25f;
            eyes.Offset = new Vector2(0, -32 + 5);

            var mouth = new Animated_Sprite(texture, "mouth-open");
            mouth.Animations.Add("mouth-open", new Animation(
                new List<Animation_Frame> {
                    new Animation_Frame(new Vector2(0, 52), new Vector2(47, 17))        { Offset = Vector2.Zero },
                    new Animation_Frame(new Vector2(0, 52+17), new Vector2(47, 17)),
                    new Animation_Frame(new Vector2(0, 52+17*2), new Vector2(47, 17)),
                    new Animation_Frame(new Vector2(0, 52+17*3), new Vector2(47, 17)),
                    new Animation_Frame(new Vector2(48, 0), new Vector2(47, 18))        { Offset = new Vector2(0, 1) },
                    new Animation_Frame(new Vector2(48, 18), new Vector2(47, 23))       { Offset = new Vector2(0, 6) },
                    new Animation_Frame(new Vector2(48, 41), new Vector2(47, 23))       { Offset = new Vector2(0, 6) },
                    new Animation_Frame(new Vector2(48, 64), new Vector2(47, 23))       { Offset = new Vector2(1, 6) },
                },
                "mouth-open"));
            mouth.Layer_Offset = 0.25f;
            mouth.Offset = new Vector2(0, 0);
                
            graphics.Animation_Components.Add("mouth", mouth);
            graphics.Animation_Components.Add("eyes", eyes);
            graphics.Animation_Components.Add("head", base_head);
            entity.Add(graphics);

            entity.Add(new Entity_Particle_Emitter(
                new Boss_1_Emitter(position, false),
                particle_world,
                false
                ) {
                Offset = new Vector2(0, 4)
            });

            entity.Add(new Health(BOSS_HEALTH));

            entity.Add(new Lua_Function(lua.DoFile("COntent/Lua/Boss_1_Ai.lua")[0] as LuaFunction, "Content/Lua/Boss_1_Ai.lua"));

            return entity;
        }
    }
}
