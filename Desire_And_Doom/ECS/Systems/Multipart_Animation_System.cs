using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Desire_And_Doom.ECS.Component;
using Desire_And_Doom.ECS.Components;

namespace Desire_And_Doom.ECS.Systems
{

    class Multipart_Animation_System : Sprite_Renderer_System
    {

        Animation_Renderer_System animation_system;

        public Multipart_Animation_System() : base()
        {
            animation_system = new Animation_Renderer_System();

            types.Clear();
            types.Add(Types.Multipart_Animation);
            types.Add(Types.Body);
        }

        public override void Update(GameTime time, Entity entity)
        {
            base.Update(time, entity);

            var multipart_animation = (Multipart_Animation) entity.Get(Types.Multipart_Animation);
            var body = (Body) entity.Get(Types.Body);
            foreach (Animated_Sprite animation in multipart_animation.Animation_Components.Values )
            {
                var tmp = new Entity();
                tmp.Add(body);
                tmp.Add(animation);

                animation_system.Update(time, tmp);
            }
        }

        public override void Draw(SpriteBatch batch, Entity entity)
        {
            base.Draw(batch, entity);

            var multipart_animation = (Multipart_Animation) entity.Get(Types.Multipart_Animation);
            var body = (Body) entity.Get(Types.Body);

            foreach ( Animated_Sprite animation in multipart_animation.Animation_Components.Values )
            {
                var tmp = new Entity();
                tmp.Add(body);
                tmp.Add(animation);

                animation_system.Draw(batch, tmp);
            }
        }
    }
}
