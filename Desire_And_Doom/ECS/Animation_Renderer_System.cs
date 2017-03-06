using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Desire_And_Doom.ECS
{
    class Animation_Renderer_System : System
    {
        public Animation_Renderer_System() : base(typeof(Body), typeof(Animated_Sprite))
        {
        }

        public override void Update(GameTime time, Entity entity)
        {
            var animation = (Animated_Sprite)entity.Get<Animated_Sprite>();
            var body = (Body)entity.Get<Body>();

            float speed = animation.Animation_IDs[animation.Current_Animation_ID];
            if (animation.Timer > speed)
            {
                animation.Timer = 0;
                animation.Current_Frame++;
            }
            else
                animation.Timer += (float)time.ElapsedGameTime.TotalSeconds;

            var frames = Assets.It.Get_Quads(animation.Current_Animation_ID).Count;
            if (animation.Current_Frame > frames - 1)
                animation.Current_Frame = 0;
        }

        public override void Draw(SpriteBatch batch, Entity entity)
        {
            var animation = (Animated_Sprite)entity.Get<Animated_Sprite>();
            var body = (Body)entity.Get<Body>();

            batch.Draw(
                animation.Texture, 
                body.Position + body.Size / 2,
                Assets.It.Get_Quads(animation.Current_Animation_ID)[animation.Current_Frame],
                animation.Color, 0, body.Size / 2, Vector2.One,(animation.Scale.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally), 1f);
        }
    }
}
