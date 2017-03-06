using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.ECS
{
    abstract class System
    {
        List<Type> types;

        public System(params Type[] _types)
        {
            types = new List<Type>();
            foreach (var t in _types)
                types.Add(t);
        }

        public bool Has_All_Types (Entity entity) {
            
            var components = entity.Get_Component_Types_List();
            foreach(var t in types)
            {
                if (!components.Contains(t)) return false;
            }
            
            return true;
        }

        public virtual void Update(GameTime time, Entity entity) { }
        public virtual void Draw(SpriteBatch batch, Entity entity) { }
    }
}
