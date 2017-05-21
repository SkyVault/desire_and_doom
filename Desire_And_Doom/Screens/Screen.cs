using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.Screens
{
    abstract class Screen
    {
        public string ID { get; protected set; }
        public Screen(string _id)
        {
            ID = _id;
        }

        public virtual void Load() { }
        public virtual void Update(GameTime time) { }
        public virtual void Draw(SpriteBatch batch) { }
        public virtual void FilteredDraw(SpriteBatch batch) { }
        public virtual void Destroy() { }
    }
}
