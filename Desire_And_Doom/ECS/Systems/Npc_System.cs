using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Desire_And_Doom.Gui;
using static Desire_And_Doom.ECS.Component;
using Microsoft.Xna.Framework.Input;

namespace Desire_And_Doom.ECS
{
    class Npc_System : System
    {
        GraphicsDeviceManager graphics;
        Game game;
        Invatory_Manager invatory_manager;

        float timer = 0;

        public Npc_System(Game game, GraphicsDeviceManager graphics, Invatory_Manager invatory_manager) : base(Component.Types.Npc, Component.Types.Body)
        {
            this.graphics = graphics;
            this.game = game;
            this.invatory_manager = invatory_manager;

        }
        
        public override void Constant_Update(GameTime time, Entity entity)
        {
            base.Constant_Update(time, entity);

            if ( timer >= 0 ) timer -= (float) time.ElapsedGameTime.TotalSeconds;

            var body = (Body) (entity.Get(Component.Types.Body));
            var anim = (Animated_Sprite) (entity.Get(Component.Types.Animation));
            var physics = (Physics) entity.Get(Component.Types.Physics);
            var npc = (Npc) entity.Get(Types.Npc);
            var player = World_Ref.Find_With_Tag("Player");

            var dialog = npc.Dialog;
            if ( player != null )
            {
                var pbody = (Body) (player.Get(Component.Types.Body));
    
                //NOTE(Dustin): Distance @hardcoded
                if ( Vector2.Distance(body.Position, pbody.Position) < 25 )
                {
                    physics.Velocity = Vector2.Zero;

                    var action_button = Input.It.Is_Key_Pressed(Microsoft.Xna.Framework.Input.Keys.Z) || 
                                        Input.It.Is_Gamepad_Button_Pressed(Buttons.A);
                    if (action_button && timer <= 0 )
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

                        var invatory = (Invatory) entity.Get(Component.Types.Invatory);
                        if (invatory != null )
                        {

                            // Draw the invatory
                            invatory_manager.Showing = dialog.Showing();
                            if ( dialog.Showing() )
                            {
                                invatory_manager.Add(invatory);
                            }
                            else invatory_manager.Remove(invatory);
                        }

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
            } else
            {
                if (dialog.Showing())
                {
                    dialog.Stop();
                    var invatory = (Invatory) entity.Get(Component.Types.Invatory);
                    if (invatory != null )
                    {
                        // Clean up the invatory
                        invatory_manager.Showing = false;
                        invatory_manager.Remove(invatory);
                    }
                }
            }
        }

        public override void Destroy(Entity entity)
        {
            base.Destroy(entity);

            invatory_manager.Remove((Invatory) entity.Get(Types.Invatory));
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

        public override void UIDraw(SpriteBatch batch, GameCamera camera,Entity entity)
        {
            base.UIDraw(batch, camera,entity);
            var npc = (Npc) entity.Get(Types.Npc);

            npc.Dialog.Draw(batch, camera);
        }
    }
}
