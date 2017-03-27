using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Desire_And_Doom.ECS.Component;

namespace Desire_And_Doom.ECS
{
    abstract class System
    {
        List<Types> types;
        public World World_Ref;

        public System(params Types[] _types)
        {
            types = new List<Types>();
            foreach (var t in _types)
                types.Add(t);
        }

        public bool Has_All_Types (Entity entity) {
            var components = entity.Get_Component_Types_List();

            foreach (var t in types) {
                if (!components.Contains(t)) return false;
            }
            
            return true;
        }

        public virtual void Load(Entity entity) { }
        public virtual void Update(GameTime time, Entity entity) { }
        public virtual void Draw(SpriteBatch batch, Entity entity) { }
        public virtual void UIDraw(SpriteBatch batch, Camera_2D camera, Entity entity) { }
    }
}
