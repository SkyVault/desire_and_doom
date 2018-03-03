using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using static Desire_And_Doom.ECS.Component;
using MonoGame.Extended;
using Desire_And_Doom.Gui;

namespace Desire_And_Doom.ECS
{
    class Invatory_System : System
    {

        public Invatory_System() : base(Types.Invatory, Types.Body)
        {
        }

        public override void Load(Entity entity)
        {
            base.Load(entity);
            
            // REMOVED: this is not how it should be
            //var inv = (Invatory) entity.Get(Types.Invatory);
            //invatory_manager.Add(inv);
        }

        public override void Destroy(Entity entity)
        {
            base.Destroy(entity);

            var inv = (Invatory) entity.Get(Types.Invatory);
        }

        public override void UIDraw(SpriteBatch batch, GameCamera camera, Entity entity)
        {
        }
    }
}
