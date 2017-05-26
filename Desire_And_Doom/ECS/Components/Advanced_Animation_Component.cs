using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desire_And_Doom.Graphics;

namespace Desire_And_Doom.ECS.Components
{
    class Advanced_Animation_Component : Component
    {
        private Dictionary<string, Animation> animations;
        private string current_animation_id;

        public Advanced_Animation_Component() : base(Types.Advanced_Animation)
        {
            animations = new Dictionary<string, Animation>();


        }
    }
}
