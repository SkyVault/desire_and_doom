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
using Desire_And_Doom.Utils;

namespace Desire_And_Doom.ECS
{
    class Npc_System : System
    {
        GraphicsDeviceManager graphics;
        Game game;
        UI_Manager invatory_manager;
        Dialog_Box dialog_box;

        //public static readonly
        float timer = 0;
        private bool talking = false;

        public Npc_System(Game game, GraphicsDeviceManager graphics, UI_Manager invatory_manager, Dialog_Box dialog_box) : base(Component.Types.Npc, Component.Types.Body)
        {
            this.graphics = graphics;
            this.game = game;
            this.invatory_manager = invatory_manager;
            this.dialog_box = dialog_box;
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

            if (player == null) return;

            var player_body = (Body)player.Get(Types.Body);

            if (Vector2.Distance(player_body.Center, body.Center) < Constants.NPC_TALKING_DISTANCE && dialog_box.IsOpen == false)
            {
                if (Input.It.Is_Key_Pressed(Keys.Z))
                {
                    Input.It.Reset_Key(Keys.Z);
                    
                    // set the speaker and the target for interactions with lua functions
                    npc.Dialog.Speaker  = entity;
                    npc.Dialog.Target   = player;

                    dialog_box.TryOpen(npc.Dialog);
                }
            }

        }

        public override void Destroy(Entity entity)
        {
            base.Destroy(entity);
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
        }
    }
}
