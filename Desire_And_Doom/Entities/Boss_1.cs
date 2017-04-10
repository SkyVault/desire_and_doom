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

namespace Desire_And_Doom.Entities
{
    class Boss_1
    {
        public static Entity Create(ContentManager content, Lua lua, World world, Vector2 position)
        {
            var entity = world.Create_Entity();

            Texture2D texture = content.Load<Texture2D>("boss_1");

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

            var eyes = new Animated_Sprite(texture, "eye-close");
            eyes.Animations.Add("eye-close", new Animation(
                new List<Animation_Frame> {
                    new Animation_Frame(new Vector2(95, 0), new Vector2(33, 5)),
                    new Animation_Frame(new Vector2(95, 5), new Vector2(33, 5)),
                    new Animation_Frame(new Vector2(95, 10), new Vector2(33, 5)),
                    new Animation_Frame(new Vector2(95, 15), new Vector2(33, 5)),
                    new Animation_Frame(new Vector2(95, 10), new Vector2(33, 5)),
                    new Animation_Frame(new Vector2(95, 5), new Vector2(33, 5)),
                },
                "eye-close"));
            eyes.Layer_Offset = 0.1f;
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
            mouth.Layer_Offset = 0.1f;
            mouth.Offset = new Vector2(0, 0);

            graphics.Animation_Components.Add("mouth", mouth);
            graphics.Animation_Components.Add("eyes", eyes);
            graphics.Animation_Components.Add("head", base_head);

            entity.Add(graphics);

            return entity;
        }
    }
}
