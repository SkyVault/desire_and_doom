using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Desire_And_Doom.ECS
{
    class Npc_System : System
    {
        GraphicsDeviceManager graphics;
        Game game;

        Dialog_Box dialog;

        float timer = 0;

        public Npc_System(Game game, GraphicsDeviceManager graphics) : base(Component.Types.Npc, Component.Types.Body)
        {
            this.graphics = graphics;
            this.game = game;

            dialog = new Dialog_Box();
            //
        }
        
        public override void Constant_Update(GameTime time, Entity entity)
        {
            base.Constant_Update(time, entity);

            if ( timer >= 0 ) timer -= (float) time.ElapsedGameTime.TotalSeconds;

            var body = (Body) (entity.Get(Component.Types.Body));
            var anim = (Animated_Sprite) (entity.Get(Component.Types.Animation));
            var physics = (Physics) entity.Get(Component.Types.Physics);
            var player = World_Ref.Find_With_Tag("Player");

            if ( player != null )
            {
                var pbody = (Body) (player.Get(Component.Types.Body));
                if ( Vector2.Distance(body.Position, pbody.Position) < 25 )
                {
                    physics.Velocity = Vector2.Zero;
                    if ( Input.It.Is_Key_Pressed(Microsoft.Xna.Framework.Input.Keys.Z) && timer <= 0 )
                    {
                        dialog.Show_Portait = true;
                        dialog.Image = anim.Texture;

                        foreach ( var id in anim.Animations.Keys )
                        {
                            if ( id.Contains("idle") )
                            {
                                dialog.Region = anim.Animations[id].Frames[0].Rectangle;
                            }
                        }

                        dialog.Animate_Toggle("#White Hello I am an #Cyan Npc! #White What can I #Yellow do #White for you?");
                        Game1.Toggle_Pause();
                        timer = 2;
                    }
                }

                var pphysics = (Physics) player.Get(Component.Types.Physics);
                if ( pphysics != null )
                {
                    if ( pphysics.Current_Speed > 2 )
                        dialog.Stop();
                }

                dialog.Update(time);
            }
        }

        public override void Draw(SpriteBatch batch, Entity entity)
        {
            base.Draw(batch, entity);
            var body = (Body)(entity.Get(Component.Types.Body));
            var sprite = (Animated_Sprite)(entity.Get(Component.Types.Animation));
            var player = World_Ref.Find_With_Tag("Player");

            if (player != null)
            {
                var pbody = (Body)(player.Get(Component.Types.Body));
                if (Vector2.Distance(body.Position, pbody.Position) < 25)
                {
                    var entities = Assets.It.Get<Texture2D>("entities");
                    batch.Draw(entities, body.Position - new Vector2(-body.Width / 2, 32), new Rectangle(458, 0, 24, 24), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
                }
            }
        }

        public override void UIDraw(SpriteBatch batch, Camera_2D camera,Entity entity)
        {
            base.UIDraw(batch, camera,entity);


            dialog.Draw(batch, camera);
        }
    }
}
