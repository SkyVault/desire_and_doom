using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Desire_And_Doom.ECS.Component;
using Desire_And_Doom.Graphics;

namespace Desire_And_Doom.ECS
{
    class Animation_Renderer_System : Sprite_Renderer_System
    {
        public Animation_Renderer_System() : base()
        {
            types.Clear();
            types.Add(Types.Body);
            types.Add(Types.Animation);
        }

        public override void Update(GameTime time, Entity entity)
        {
            var animation = (Animated_Sprite)entity.Get(Types.Animation);
            var body = (Body)entity.Get(Types.Body);

            if ( !animation.Playing ) return;

            animation.Timer += (float)time.ElapsedGameTime.TotalSeconds;

            if (animation.Animations.ContainsKey(animation.Current_Animation_ID) == false)
                throw new Exception("Animation does not contain the key: "+ animation.Current_Animation_ID);

            Animation current_animation     = animation.Animations[animation.Current_Animation_ID];

            if (animation.Current_Frame > current_animation.Frames.Count - 1)
                animation.Current_Frame = 0;

            Animation_Frame current_frame   = current_animation.Frames[animation.Current_Frame];

            if (animation.Timer >= current_frame.Frame_Time)
            {
                animation.Timer = 0;
                animation.Current_Frame++;
                if (animation.Current_Frame > current_animation.Frames.Count - 1)
                    animation.Current_Frame = 0;
            }

            if (animation.Current_Frame == current_animation.Frames.Count - 1)
            {
                animation.Force_Animation = false;
                animation.Animation_End = true;
            }
            else
                animation.Animation_End = false;

            // flashes red when the timer is high
            if (animation.Flash_Timer > 0 )
            {
                if ( animation.Color == Color.White )
                {
                    animation.Color = Color.Red;
                }else
                {
                    animation.Color = Color.White;
                }

                animation.Flash_Timer -= (float) time.ElapsedGameTime.TotalSeconds;
            }

        }

        public override void Draw(SpriteBatch batch, Entity entity)
        {
            var animation = (Animated_Sprite)entity.Get(Types.Animation);
            var body = (Body)entity.Get(Types.Body);

            // TODO(Dustin): Lets not look at this global that may or may not change if we forget to update it.
            animation.Layer = Get_Layer(body);

            Animation current_animation = animation.Animations[animation.Current_Animation_ID];
            if (animation.Current_Frame > current_animation.Frames.Count - 1)
                animation.Current_Frame = 0;

            var frame = current_animation.Frames[animation.Current_Frame];
            var quad = frame.Rectangle;
            var side = animation.Scale.X > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            
            var offset = Vector2.Zero;
            if ( side == SpriteEffects.None )
                offset = current_animation.Right_Face_Offset;
            else
                offset = current_animation.Left_Face_Offset;
            
            batch.Draw(
                animation.Texture,
                
                body.Position - new Vector2(quad.Width / 2 - body.Width / 2, quad.Height - body.Height) 
                + current_animation.Offset 
                + offset 
                + animation.Offset
                + frame.Offset,
                
                quad,
                animation.Color,
                0,
                Vector2.Zero,
                Vector2.One,
                side,
                animation.Layer + animation.Layer_Offset
                );
        }
    }
}
