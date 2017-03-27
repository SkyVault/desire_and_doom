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

        public Npc_System(Game game, GraphicsDeviceManager graphics) : base(Component.Types.Npc, Component.Types.Body)
        {
            this.graphics = graphics;
            this.game = game;

            dialog = new Dialog_Box();
            //
        }
        
        public override void Update(GameTime time, Entity entity)
        {
            base.Update(time, entity);

            var body = (Body)(entity.Get(Component.Types.Body));
            var sprite = (Sprite)(entity.Get(Component.Types.Sprite));
            var player = World_Ref.Find_With_Tag("Player");
            if (player != null)
            {
                var pbody = (Body)(player.Get(Component.Types.Body));
                if (Vector2.Distance(body.Position, pbody.Position) < 25)
                {
                    if (Input.It.Is_Key_Pressed(Microsoft.Xna.Framework.Input.Keys.Z))
                    {
                        dialog.Animate_Toggle("Hello I am an <Cyan> Npc!");
                    }
                }

                dialog.Update(time);
            }
        }

        public override void Draw(SpriteBatch batch, Entity entity)
        {
            base.Draw(batch, entity);
            var body = (Body)(entity.Get(Component.Types.Body));
            var sprite = (Sprite)(entity.Get(Component.Types.Sprite));
            var player = World_Ref.Find_With_Tag("Player");
            if (player != null)
            {
                var pbody = (Body)(player.Get(Component.Types.Body));
                if (Vector2.Distance(body.Position, pbody.Position) < 25)
                {
                    var entities = Assets.It.Get<Texture2D>("entities");
                    batch.Draw(entities, body.Position - new Vector2(-body.Width / 2, sprite.Quad.Height + 14), new Rectangle(458, 0, 24, 24), Color.White, 0, Vector2.Zero, 0.7f, SpriteEffects.None, 1);
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
