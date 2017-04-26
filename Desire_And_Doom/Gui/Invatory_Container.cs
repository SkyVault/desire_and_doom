using Desire_And_Doom.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desire_And_Doom.Gui
{
    class Invatory_Container
    {
        public List<Invatory> Invatories { get; set; }
        int current_x = 0;
        int current_y = 0;

        public Invatory_Container()
        {
            Invatories = new List<Invatory>();
        }

        public void Update(GameTime time)
        {
            
        }
        
    }
}
